using Microsoft.AspNetCore.Mvc;
using reto2_api.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoUsuarioController : ControllerBase
    {
        private readonly IArchivoUsuarioService _archivoUsuarioService;

        public ArchivoUsuarioController(IArchivoUsuarioService archivoUsuarioService)
        {
            _archivoUsuarioService = archivoUsuarioService;
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<List<int>>> GetArchivosGuardadosPorUsuario(int idUsuario)
        {
            var archivos = await _archivoUsuarioService.GetArchivosGuardadosPorUsuarioAsync(idUsuario);
            if (archivos == null || archivos.Count == 0)
                return NotFound("no se encontraron archivos guardados para este usuario.");

            return Ok(archivos);
        }

        [HttpPost]
        public async Task<IActionResult> AddArchivo(ArchivoUsuario archivoUsuario)
        {
            await _archivoUsuarioService.AddAsync(archivoUsuario);
            return Ok("archivo añadido a la lista de guardados correctamente.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArchivo([FromQuery] int idUsuario, [FromQuery] int idArchivo)
        {
            await _archivoUsuarioService.DeleteAsync(idUsuario, idArchivo);
            return Ok("archivo eliminado de la lista de guardados correctamente.");
        }
    }
}
