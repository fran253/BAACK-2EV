using Microsoft.AspNetCore.Mvc;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CursoController : ControllerBase
   {
    private static List<Curso> cursos = new List<Curso>();

    private readonly ICursoService _serviceCurso;

    public CursoController(ICursoService service)
        {
            _serviceCurso = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Curso>>> GetCurso()
        {
            var cursos = await _serviceCurso.GetAllAsync();
            return Ok(cursos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _serviceCurso.GetByIdAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> CreateCurso(Curso curso)
        {
            await _serviceCurso.AddAsync(curso);
            return CreatedAtAction(nameof(GetCurso), new { id = curso.IdCurso }, curso);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurso(int id, Curso updatedCurso)
        {
            var existingCurso = await _serviceCurso.GetByIdAsync(id);
            if (existingCurso == null)
            {
                return NotFound();
            }

            // Actualizar el curso existente
            existingCurso.Nombre = updatedCurso.Nombre;
            existingCurso.Descripcion = updatedCurso.Descripcion;
            existingCurso.Imagen = updatedCurso.Imagen;

            await _serviceCurso.UpdateAsync(existingCurso);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteCurso(int id)
       {
           var curso = await _serviceCurso.GetByIdAsync(id);
           if (curso == null)
           {
               return NotFound();
           }
           await _serviceCurso.DeleteAsync(id);
           return NoContent();
       }
   }
}