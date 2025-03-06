using Microsoft.AspNetCore.Mvc;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class TestController : ControllerBase
   {
    private static List<Test> tests = new List<Test>();

    private readonly ITestService _serviceTest;

    public TestController(ITestService service)
        {
            _serviceTest = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Test>>> GetTest()
        {
            var tests = await _serviceTest.GetAllAsync();
            return Ok(tests);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var test = await _serviceTest.GetByIdAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }

        [HttpPost]
        public async Task<ActionResult<Test>> CreateTest(Test test)
        {
            await _serviceTest.AddAsync(test);
            return CreatedAtAction(nameof(GetTest), new { id = test.IdTest }, test);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(int id, [FromBody] Test updatedTest)
        {
            if (updatedTest == null || id != updatedTest.IdTest)
            {
                return BadRequest("Los datos del test no son válidos.");
            }

            var existingTest = await _serviceTest.GetByIdAsync(id);
            if (existingTest == null)
            {
                return NotFound($"No se encontró el test con ID {id}.");
            }

            try
            {
                // Actualizar los campos correctos de Test
                existingTest.Titulo = updatedTest.Titulo;
                existingTest.FechaCreacion = updatedTest.FechaCreacion;
                existingTest.IdTemario = updatedTest.IdTemario;

                await _serviceTest.UpdateAsync(existingTest);
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
           var curso = await _serviceTest.GetByIdAsync(id);
           if (curso == null)
           {
               return NotFound();
           }
           await _serviceTest.DeleteAsync(id);
           return NoContent();
       }

        ///METODO TESTS DE UN TEMA
         [HttpGet("temario/{idTemario}")]
        public async Task<ActionResult<List<Test>>> GetByTemarioId(int idTemario)
        {
            var tests = await _serviceTest.GetByTemarioIdAsync(idTemario);
            if (tests == null || tests.Count == 0)
                return NotFound("no se encontraron tests para este temario.");

            return Ok(tests);
        }
   }
}