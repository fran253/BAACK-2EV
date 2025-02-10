using Microsoft.AspNetCore.Mvc;
using reto2_api.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UsuarioController : ControllerBase
   {
       private readonly IUsuarioService _usuarioService;

       public UsuarioController(IUsuarioService usuarioService)
       {
           _usuarioService = usuarioService;
       }

       [HttpGet]
       public async Task<ActionResult<List<Usuario>>> GetUsuarios()
       {
           var usuarios = await _usuarioService.GetAllUsuariosAsync();
           return Ok(usuarios);
       }

       [HttpGet("{id}")]
       public async Task<ActionResult<Usuario>> GetUsuario(int id)
       {
           var usuario = await _usuarioService.GetByIdAsync(id);
           if (usuario == null)
           {
               return NotFound();
           }
           return Ok(usuario);
       }

       // Crear un nuevo usuario
       [HttpPost]
       public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
       {
           await _usuarioService.AddAsync(usuario);
           return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
       }

       // Actualizar un usuario
       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateUsuario(int id, Usuario updatedUsuario)
       {
           var existingUsuario = await _usuarioService.GetByIdAsync(id);
           if (existingUsuario == null)
           {
               return NotFound();
           }

           // Actualizar el usuario existente
           existingUsuario.Nombre = updatedUsuario.Nombre;
           existingUsuario.Apellido = updatedUsuario.Apellido;
           existingUsuario.Gmail = updatedUsuario.Gmail;
           existingUsuario.Telefono = updatedUsuario.Telefono;
           existingUsuario.Contraseña = updatedUsuario.Contraseña;
           existingUsuario.Rol.IdRol = updatedUsuario.Rol.IdRol;

           await _usuarioService.UpdateAsync(existingUsuario);
           return NoContent();
       }

       // Eliminar un usuario
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteUsuario(int id)
       {
           var usuario = await _usuarioService.GetByIdAsync(id);
           if (usuario == null)
           {
               return NotFound();
           }
           await _usuarioService.DeleteAsync(id);
           return NoContent();
       }

       // Inicializar datos de prueba
       [HttpPost("inicializar")]
       public async Task<IActionResult> InicializarDatos()
       {
           await _usuarioService.InicializarDatosAsync();
           return Ok("datos inicializados correctamente.");
       }
   }
}
