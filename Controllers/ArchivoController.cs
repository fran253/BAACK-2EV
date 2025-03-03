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
                return NoContent(); // Código 204, actualización exitosa sin contenido
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
                return NotFound("no se encontraron archivos para este temario.");

            return Ok(archivos);
        }
        // ✅ Nuevo método para subir archivos físicos
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

        // Ruta donde se guardará el archivo
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName = $"{Guid.NewGuid()}_{archivo.FileName}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        // Guardar archivo en el servidor
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await archivo.CopyToAsync(stream);
        }

        // URL accesible para el archivo
        var fileUrl = $"/archivos/{fileName}";

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

   }
}