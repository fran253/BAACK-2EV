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

        [HttpPost]
        public async Task<ActionResult<Comentario>> CreateComentario(Comentario comentario)
        {
            await _serviceComentario.AddAsync(comentario);
            return CreatedAtAction(nameof(GetComentario), new { id = comentario.IdComentario }, comentario);
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
   }
}