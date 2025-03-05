using Microsoft.AspNetCore.Mvc;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ArchivoController : ControllerBase
   {
    private static List<Archivo> archivos = new List<Archivo>();

    private readonly IArchivoService _serviceArchivo;

    public ArchivoController(IArchivoService service)
        {
            _serviceArchivo = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Archivo>>> GetArchivo()
        {
            var archivos = await _serviceArchivo.GetAllAsync();
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


        ///Cambio necesario///
  
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

        // Nuevo método para subir archivos físicos
        [HttpPost("upload")]
        public async Task<IActionResult> UploadArchivo(
            IFormFile archivo, 
            [FromForm] string titulo, 
            [FromForm] string tipo, 
            [FromForm] int idUsuario, 
            [FromForm] int idTemario)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha subido ningún archivo.");
            }

            // Ruta donde se guardará el archivo en wwwroot/archivos/
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos");

            // Crear la carpeta si no existe
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Guardar el archivo con su extensión real
            var extension = Path.GetExtension(archivo.FileName);
            if (string.IsNullOrEmpty(extension))
            {
                return BadRequest("El archivo no tiene una extensión válida.");
            }

            var fileName = $"{Guid.NewGuid()}{extension}"; 
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Guardar el archivo en el servidor
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Crear URL accesible
            var fileUrl = $"/archivos/{fileName}";

            // Guardar en la base de datos
            var nuevoArchivo = new Archivo
            {
                Titulo = titulo,
                Url = fileUrl, // URL corregida
                Tipo = tipo,
                FechaCreacion = DateTime.UtcNow,
                IdUsuario = idUsuario,
                IdTemario = idTemario
            };

            await _serviceArchivo.AddAsync(nuevoArchivo);

            return Ok(new { mensaje = "Archivo subido correctamente", archivoUrl = fileUrl });
        }


    }
}