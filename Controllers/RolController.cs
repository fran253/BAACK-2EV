using reto2_api.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class RolController : ControllerBase
   {
       private readonly IRolService _rolService;

       public RolController(IRolService rolService)
       {
           _rolService = rolService;
       }

       // Obtener todos los roles
       [HttpGet]
       public async Task<ActionResult<List<Rol>>> GetRoles()
       {
           var roles = await _rolService.GetAllRolesAsync();
           return Ok(roles);
       }

       // Obtener un rol por su ID
       [HttpGet("{id}")]
       public async Task<ActionResult<Rol>> GetRol(int id)
       {
           var rol = await _rolService.GetByIdAsync(id);
           if (rol == null)
           {
               return NotFound();
           }
           return Ok(rol);
       }

       // Crear un nuevo rol
       [HttpPost]
       public async Task<ActionResult<Rol>> CreateRol(Rol rol)
       {
           await _rolService.AddAsync(rol);
           return CreatedAtAction(nameof(GetRol), new { id = rol.IdRol }, rol);
       }

       // Actualizar un rol
       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateRol(int id, Rol updatedRol)
       {
           var existingRol = await _rolService.GetByIdAsync(id);
           if (existingRol == null)
           {
               return NotFound();
           }

           // Actualizar el rol existente
           existingRol.Nombre = updatedRol.Nombre;

           await _rolService.UpdateAsync(existingRol);
           return NoContent();
       }

       // Eliminar un rol
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteRol(int id)
       {
           var rol = await _rolService.GetByIdAsync(id);
           if (rol == null)
           {
               return NotFound();
           }
           await _rolService.DeleteAsync(id);
           return NoContent();
       }

       // Inicializar datos de prueba
       [HttpPost("inicializar")]
       public async Task<IActionResult> InicializarDatos()
       {
           await _rolService.InicializarDatosAsync();
           return Ok("datos inicializados correctamente.");
       }
   }
}
