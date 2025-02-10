using Microsoft.AspNetCore.Mvc;
using reto2_api.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpcionController : ControllerBase
    {
        private readonly IOpcionService _opcionService;

        public OpcionController(IOpcionService opcionService)
        {
            _opcionService = opcionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Opcion>>> GetOpciones()
        {
            var opciones = await _opcionService.GetAllAsync();
            return Ok(opciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Opcion>> GetOpcion(int id)
        {
            var opcion = await _opcionService.GetByIdAsync(id);
            if (opcion == null)
            {
                return NotFound("opción no encontrada.");
            }
            return Ok(opcion);
        }

        [HttpPost]
        public async Task<ActionResult<Opcion>> CreateOpcion(Opcion opcion)
        {
            await _opcionService.AddAsync(opcion);
            return CreatedAtAction(nameof(GetOpcion), new { id = opcion.IdOpcion }, opcion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOpcion(int id, Opcion updatedOpcion)
        {
            var existingOpcion = await _opcionService.GetByIdAsync(id);
            if (existingOpcion == null)
            {
                return NotFound("opción no encontrada.");
            }

            existingOpcion.Texto = updatedOpcion.Texto;
            existingOpcion.EsCorrecta = updatedOpcion.EsCorrecta;
            existingOpcion.Pregunta = updatedOpcion.Pregunta;

            await _opcionService.UpdateAsync(existingOpcion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpcion(int id)
        {
            var opcion = await _opcionService.GetByIdAsync(id);
            if (opcion == null)
            {
                return NotFound("opción no encontrada.");
            }
            await _opcionService.DeleteAsync(id);
            return NoContent();
        }

        ///METODO OPCIONES DE PREGUNTA
        [HttpGet("pregunta/{idPregunta}")]
        public async Task<ActionResult<List<Opcion>>> GetByPreguntaId(int idPregunta)
        {
            var opciones = await _opcionService.GetByPreguntaIdAsync(idPregunta);
            if (opciones == null || opciones.Count == 0)
                return NotFound("no se encontraron opciones para esta pregunta.");

            return Ok(opciones);
        }
    }
}
