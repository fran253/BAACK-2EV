using Microsoft.AspNetCore.Mvc;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ComentarioController : ControllerBase
   {
    private static List<Comentario> comentarios = new List<Comentario>();

    private readonly IComentarioService _serviceComentario;

    public ComentarioController(IComentarioService service)
        {
            _serviceComentario = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Comentario>>> GetComentario()
        {
            var comentarios = await _serviceComentario.GetAllAsync();
            return Ok(comentarios);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
            var comentario = await _serviceComentario.GetByIdAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }

        [HttpPost("publicar")]
        public async Task<IActionResult> CreateComentario([FromBody] Comentario comentario)
        {
            if (comentario == null)
                return BadRequest("El comentario no puede ser nulo.");

            if (comentario.IdUsuario <= 0)
                return BadRequest("El ID del usuario no es válido.");

            if (comentario.IdArchivo <= 0)
                return BadRequest("El ID del archivo no es válido.");

            if (string.IsNullOrWhiteSpace(comentario.Contenido))
                return BadRequest("El comentario no puede estar vacío.");

            try
            {
                await _serviceComentario.AddAsync(comentario);
                return CreatedAtAction(nameof(CreateComentario), new { id = comentario.IdComentario }, comentario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComentario(int id, [FromBody] Comentario UpdateComentario)
        {
            if (UpdateComentario == null || id != UpdateComentario.IdComentario)
            {
                return BadRequest("Los datos del comentario no son válidos.");
            }

            var existingComentario = await _serviceComentario.GetByIdAsync(id);
            if (existingComentario == null)
            {
                return NotFound($"No se encontró el comentario con ID {id}.");
            }

            try
            {
                // Actualizar los campos
                existingComentario.Contenido = UpdateComentario.Contenido;
                existingComentario.FechaCreacion = UpdateComentario.FechaCreacion;
                existingComentario.IdUsuario = UpdateComentario.IdUsuario;
                existingComentario.IdArchivo = UpdateComentario.IdArchivo;

                await _serviceComentario.UpdateAsync(existingComentario);
                return NoContent(); // Código 204, actualización exitosa sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteComentario(int id)
       {
           var comentario = await _serviceComentario.GetByIdAsync(id);
           if (comentario == null)
           {
               return NotFound();
           }
           await _serviceComentario.DeleteAsync(id);
           return NoContent();
       }

       [HttpGet("archivo/{idArchivo}")]
        public async Task<ActionResult<List<Comentario>>> GetComentariosByArchivo(int idArchivo)
        {
            try
            {
                var comentarios = await _serviceComentario.GetByArchivoIdAsync(idArchivo);
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
   }
}