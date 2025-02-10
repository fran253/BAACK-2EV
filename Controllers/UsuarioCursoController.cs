using Microsoft.AspNetCore.Mvc;
using reto2_api.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioCursoController : ControllerBase
    {
        private readonly IUsuarioCursoService _usuarioCursoService;

        public UsuarioCursoController(IUsuarioCursoService usuarioCursoService)
        {
            _usuarioCursoService = usuarioCursoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioCurso>>> GetAll()
        {
            var inscripciones = await _usuarioCursoService.GetAllAsync();
            return Ok(inscripciones);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<List<UsuarioCurso>>> GetByUsuarioId(int idUsuario)
        {
            var inscripciones = await _usuarioCursoService.GetByUsuarioIdAsync(idUsuario);
            return Ok(inscripciones);
        }

        [HttpGet("curso/{idCurso}")]
        public async Task<ActionResult<List<UsuarioCurso>>> GetByCursoId(int idCurso)
        {
            var inscripciones = await _usuarioCursoService.GetByCursoIdAsync(idCurso);
            return Ok(inscripciones);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UsuarioCurso usuarioCurso)
        {
            await _usuarioCursoService.AddAsync(usuarioCurso);
            return Ok("inscripción creada con éxito.");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idUsuario, int idCurso)
        {
            await _usuarioCursoService.DeleteAsync(idUsuario, idCurso);
            return Ok("inscripción eliminada con éxito.");
        }
    }
}
