using Microsoft.AspNetCore.Mvc;
using reto2_api.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {
        private readonly IPreguntaService _preguntaService;

        public PreguntaController(IPreguntaService preguntaService)
        {
            _preguntaService = preguntaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pregunta>>> GetPreguntas()
        {
            var preguntas = await _preguntaService.GetAllAsync();
            return Ok(preguntas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pregunta>> GetPregunta(int id)
        {
            var pregunta = await _preguntaService.GetByIdAsync(id);
            if (pregunta == null)
            {
                return NotFound();
            }
            return Ok(pregunta);
        }

        [HttpPost]
        public async Task<ActionResult<Pregunta>> CreatePregunta(Pregunta pregunta)
        {
            await _preguntaService.AddAsync(pregunta);
            return CreatedAtAction(nameof(GetPregunta), new { id = pregunta.IdPregunta }, pregunta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePregunta(int id, Pregunta updatedPregunta)
        {
            var existingPregunta = await _preguntaService.GetByIdAsync(id);
            if (existingPregunta == null)
            {
                return NotFound();
            }

            existingPregunta.Enunciado = updatedPregunta.Enunciado;
            existingPregunta.Test = updatedPregunta.Test;

            await _preguntaService.UpdateAsync(existingPregunta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePregunta(int id)
        {
            var pregunta = await _preguntaService.GetByIdAsync(id);
            if (pregunta == null)
            {
                return NotFound();
            }
            await _preguntaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
