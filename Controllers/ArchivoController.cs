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
   }
}