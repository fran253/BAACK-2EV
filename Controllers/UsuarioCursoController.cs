using Microsoft.AspNetCore.Mvc;
using reto2_api.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Controllers
{
    public class UsuarioCursoDTO
    {
        public int IdUsuario { get; set; }
        public int IdCurso { get; set; }
    }

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
        public async Task<ActionResult<List<UsuarioCurso>>> GetByIdCurso(int idCurso)
        {
            var inscripciones = await _usuarioCursoService.GetByIdCursoAsync(idCurso);
            return Ok(inscripciones);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UsuarioCursoDTO dto)
        {
            try
            {
                // Crear un objeto UsuarioCurso usando solo los IDs
                var usuarioCurso = new UsuarioCurso(dto.IdUsuario, dto.IdCurso);
                
                await _usuarioCursoService.AddAsync(usuarioCurso);
                return Ok("inscripción creada con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idUsuario, int idCurso)
        {
            await _usuarioCursoService.DeleteAsync(idUsuario, idCurso);
            return Ok("inscripción eliminada con éxito.");
        }
    }
}