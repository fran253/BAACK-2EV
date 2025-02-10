using Microsoft.AspNetCore.Mvc;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class TemarioController : ControllerBase
   {
    private static List<Temario> temarios = new List<Temario>();

    private readonly ITemarioService _serviceTemario;

    public TemarioController(ITemarioService service)
        {
            _serviceTemario = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Temario>>> GetTemario()
        {
            var temarios = await _serviceTemario.GetAllAsync();
            return Ok(temarios);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Temario>> GetTemario(int id)
        {
            var temario = await _serviceTemario.GetByIdAsync(id);
            if (temario == null)
            {
                return NotFound();
            }
            return Ok(temario);
        }

        [HttpPost]
        public async Task<ActionResult<Temario>> CreateTemario(Temario temario)
        {
            await _serviceTemario.AddAsync(temario);
            return CreatedAtAction(nameof(GetTemario), new { id = temario.IdTemario }, temario);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemario(int id, [FromBody] Temario updatedTemario)
        {
            if (updatedTemario == null || id != updatedTemario.IdTemario)
            {
                return BadRequest("Los datos del temario no son válidos.");
            }

            var existingTemario = await _serviceTemario.GetByIdAsync(id);
            if (existingTemario == null)
            {
                return NotFound($"No se encontró el temario con ID {id}.");
            }

            try
            {
                // Actualizar los campos correctos de Temario
                existingTemario.Titulo = updatedTemario.Titulo;
                existingTemario.Descripcion = updatedTemario.Descripcion;
                existingTemario.IdAsignatura = updatedTemario.IdAsignatura;

                await _serviceTemario.UpdateAsync(existingTemario);
                return NoContent(); // Código 204, éxito sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteTest(int id)
       {
           var curso = await _serviceTemario.GetByIdAsync(id);
           if (curso == null)
           {
               return NotFound();
           }
           await _serviceTemario.DeleteAsync(id);
           return NoContent();
       }
   }
}