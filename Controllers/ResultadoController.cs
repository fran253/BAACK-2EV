using Microsoft.AspNetCore.Mvc;
using reto2_api.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadoController : ControllerBase
    {
        private readonly IResultadoService _resultadoService;

        public ResultadoController(IResultadoService resultadoService)
        {
            _resultadoService = resultadoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Resultado>>> GetResultados()
        {
            var resultados = await _resultadoService.GetAllAsync();
            return Ok(resultados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resultado>> GetResultado(int id)
        {
            var resultado = await _resultadoService.GetByIdAsync(id);
            if (resultado == null)
            {
                return NotFound("resultado no encontrado.");
            }
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<Resultado>> CreateResultado(Resultado resultado)
        {
            await _resultadoService.AddAsync(resultado);
            return CreatedAtAction(nameof(GetResultado), new { id = resultado.IdResultado }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResultado(int id, Resultado updatedResultado)
        {
            var existingResultado = await _resultadoService.GetByIdAsync(id);
            if (existingResultado == null)
            {
                return NotFound("resultado no encontrado.");
            }

            existingResultado.Puntuacion = updatedResultado.Puntuacion;
            existingResultado.Fecha = updatedResultado.Fecha;
            existingResultado.Usuario = updatedResultado.Usuario;
            existingResultado.Pregunta = updatedResultado.Pregunta;

            await _resultadoService.UpdateAsync(existingResultado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResultado(int id)
        {
            var resultado = await _resultadoService.GetByIdAsync(id);
            if (resultado == null)
            {
                return NotFound("resultado no encontrado.");
            }
            await _resultadoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
