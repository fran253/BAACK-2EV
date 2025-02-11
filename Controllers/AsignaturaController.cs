using Microsoft.AspNetCore.Mvc;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AsignaturaController : ControllerBase
   {
    private static List<Asignatura> asignaturas = new List<Asignatura>();

    private readonly IAsignaturaService _serviceAsignatura;

    public AsignaturaController(IAsignaturaService service)
        {
            _serviceAsignatura = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Asignatura>>> GetAsignatura()
        {
            var asignaturas = await _serviceAsignatura.GetAllAsync();
            return Ok(asignaturas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> GetAsignatura(int id)
        {
            var asignatura = await _serviceAsignatura.GetByIdAsync(id);
            if (asignatura == null)
            {
                return NotFound();
            }
            return Ok(asignatura);
        }

        [HttpPost]
        public async Task<ActionResult<Asignatura>> CreateAsignatura(Asignatura asignatura)
        {
            await _serviceAsignatura.AddAsync(asignatura);
            return CreatedAtAction(nameof(GetAsignatura), new { id = asignatura.IdAsignatura }, asignatura);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsignatura(int id, [FromBody] Asignatura updatedAsignatura)
        {
            if (updatedAsignatura == null || id != updatedAsignatura.IdAsignatura)
            {
                return BadRequest("Los datos de la asignatura no son válidos.");
            }

            var existingAsignatura = await _serviceAsignatura.GetByIdAsync(id);
            if (existingAsignatura == null)
            {
                return NotFound($"No se encontró la asignatura con ID {id}.");
            }

            try
            {
                // Actualizar los campos
                existingAsignatura.Nombre = updatedAsignatura.Nombre;
                existingAsignatura.Descripcion = updatedAsignatura.Descripcion;
                existingAsignatura.Imagen = updatedAsignatura.Imagen;
                existingAsignatura.FechaCreacion = updatedAsignatura.FechaCreacion;
                existingAsignatura.CursoId = updatedAsignatura.CursoId;

                await _serviceAsignatura.UpdateAsync(existingAsignatura);
                return NoContent(); // Código 204, actualización exitosa sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteAsignatura(int id)
       {
           var asignatura = await _serviceAsignatura.GetByIdAsync(id);
           if (asignatura == null)
           {
               return NotFound();
           }
           await _serviceAsignatura.DeleteAsync(id);
           return NoContent();
       }

       //Metodo Mostrar todas las asignaturas de un curso
        [HttpGet("curso/{cursoId}")]
        public async Task<ActionResult<List<Asignatura>>> GetByCursoId(int cursoId)
        {
            var asignaturas = await _serviceAsignatura.GetByCursoIdAsync(cursoId);
            if (asignaturas == null || asignaturas.Count == 0)
                return NotFound("no se encontraron asignaturas para este curso.");

            return Ok(asignaturas);
        }
   }
}