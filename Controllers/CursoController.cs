using Microsoft.AspNetCore.Mvc;
using reto2_api.Service;

namespace reto2_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCursos()
        {
            var cursos = await _cursoService.GetAllAsync();
            return Ok(cursos);
        }
    }
}
