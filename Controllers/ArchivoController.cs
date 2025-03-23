using Microsoft.AspNetCore.Mvc;
using reto2_api.Repositories;
using reto2_api.Service;
using Microsoft.AspNetCore.Http.Features;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon;


namespace reto2_api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ArchivoController : ControllerBase
   {
    private static List<Archivo> archivos = new List<Archivo>();

    private readonly IArchivoService _serviceArchivo;
    private readonly IConfiguration _configuration;

    public ArchivoController(IArchivoService service, IConfiguration configuration)
        {
            _serviceArchivo = service;
            _configuration = configuration;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Archivo>>> GetArchivo()
        {
            var archivos = await _serviceArchivo.GetAllAsync();
            return Ok(archivos);
        }

        [HttpGet("NombreUsuario")]
        public async Task<ActionResult<List<Archivo>>> GetNombreUsuario()
        {
            var archivos = await _serviceArchivo.GetNombreUsuarioAsync();
            return Ok(archivos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Archivo>> GetArchivo(int id)
        {
            var archivo = await _serviceArchivo.GetByIdAsync(id);
            if (archivo == null)
            {
                return NotFound();  
            }
            return Ok(archivo);
        }

        //METODO PARA EL FILTRADO POR TIPO DE ARCHIVO
        [HttpGet("tipo/{tipo}/temario/{idTemario}")]
        public async Task<ActionResult<List<Archivo>>> GetByTipoAndTemarioAsync(string tipo, int idTemario)
        {
            var archivos = await _serviceArchivo.GetByTipoAndTemarioAsync(tipo, idTemario);
            
            if (archivos == null || archivos.Count == 0)
                return NotFound($"No se encontraron archivos de tipo '{tipo}' para el temario con ID {idTemario}.");

            // Asegurar que los archivos existen antes de enviarlos al frontend
            string archivosFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            foreach (var archivo in archivos)
            {
                string filePath = Path.Combine(archivosFolder, archivo.Url.TrimStart('/'));
                
                if (!System.IO.File.Exists(filePath))
                {
                    archivo.Url = null; 
                }
            }

            return Ok(archivos);
        }

        [HttpPost]
        public async Task<ActionResult<Archivo>> CreateArchivo(Archivo archivo)
        {
            await _serviceArchivo.AddAsync(archivo);
            return CreatedAtAction(nameof(GetArchivo), new { id = archivo.IdArchivo }, archivo);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArchivo(int id, [FromBody] Archivo updatedArchivo)
        {
            if (updatedArchivo == null || id != updatedArchivo.IdArchivo)
            {
                return BadRequest("Los datos del archivo no son válidos.");
            }

            var existingArchivo = await _serviceArchivo.GetByIdAsync(id);
            if (existingArchivo == null)
            {
                return NotFound($"No se encontró el archivo con ID {id}.");
            }

            try
            {
                // Actualizar los campos
                existingArchivo.Titulo = updatedArchivo.Titulo;
                existingArchivo.Url = updatedArchivo.Url;
                existingArchivo.Tipo = updatedArchivo.Tipo;
                existingArchivo.FechaCreacion = updatedArchivo.FechaCreacion;
                existingArchivo.IdUsuario = updatedArchivo.IdUsuario;
                existingArchivo.IdTemario = updatedArchivo.IdTemario;

                await _serviceArchivo.UpdateAsync(existingArchivo);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteArchivo(int id)
       {
           var archivo = await _serviceArchivo.GetByIdAsync(id);
           if (archivo == null)
           {
               return NotFound();
           }
           await _serviceArchivo.DeleteAsync(id);
           return NoContent();
       }
       
       ///METODO ARCHIVOS DE UN TEMA
        [HttpGet("temario/{idTemario}")]
        public async Task<ActionResult<List<Archivo>>> GetByTemarioId(int idTemario)
        {
            var archivos = await _serviceArchivo.GetByTemarioIdAsync(idTemario);
            
            if (archivos == null || archivos.Count == 0)
                return NotFound("No se encontraron archivos para este temario.");

            return Ok(archivos);
        }

        ///METODO ARCHIVOS DE UN USUARIO
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<List<Archivo>>> GetByUsuarioId(int idUsuario)
        {
            var archivos = await _serviceArchivo.GetByUsuarioIdAsync(idUsuario);
            
            if (archivos == null || archivos.Count == 0)
                return NotFound("No se encontraron archivos para este usuario.");

            // Asegurar que los archivos existen antes de enviarlos al frontend
            string archivosFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            foreach (var archivo in archivos)
            {
                string filePath = Path.Combine(archivosFolder, archivo.Url.TrimStart('/'));
                
                if (!System.IO.File.Exists(filePath))
                {
                    archivo.Url = null; 
                }
            }

            return Ok(archivos);
        }

        // Método mejorado para subir archivos físicos
      [HttpPost("upload")]
        [RequestSizeLimit(500 * 1024 * 1024)] // 500MB
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> UploadArchivo(
            IFormFile archivo,
            [FromForm] string titulo,
            [FromForm] string tipo,
            [FromForm] int idUsuario,
            [FromForm] int idTemario)
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                    return BadRequest("No se ha subido ningún archivo.");

                // Config AWS
                var awsAccessKey = _configuration["AWS:AccessKey"];
                var awsSecretKey = _configuration["AWS:SecretKey"];
                var bucketName = _configuration["AWS:BucketName"];
                var region = Amazon.RegionEndpoint.GetBySystemName(_configuration["AWS:Region"]);

                var s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, region);

                // Crear nombre único para el archivo
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = archivo.OpenReadStream(),
                    Key = fileName,
                    BucketName = bucketName,
                    ContentType = archivo.ContentType,
                };

                using var fileTransferUtility = new TransferUtility(s3Client);
                await fileTransferUtility.UploadAsync(uploadRequest);

                // Crear URL pública
                var fileUrl = $"https://{bucketName}.s3.{region.SystemName}.amazonaws.com/{fileName}";

                // Guardar en la base de datos
                var nuevoArchivo = new Archivo
                {
                    Titulo = titulo,
                    Url = fileUrl,
                    Tipo = tipo,
                    FechaCreacion = DateTime.UtcNow,
                    IdUsuario = idUsuario,
                    IdTemario = idTemario
                };

                await _serviceArchivo.AddAsync(nuevoArchivo);

                return Ok(new { mensaje = "Archivo subido correctamente", archivoUrl = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al subir archivo: {ex.Message}");
            }
        }
    }
}