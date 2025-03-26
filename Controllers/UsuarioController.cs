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

       // GET: api/Usuario
       [HttpGet]
       public async Task<ActionResult<List<Usuario>>> GetUsuarios()
       {
           var usuarios = await _usuarioService.GetAllUsuariosAsync();
           return Ok(usuarios);
       }

        [HttpGet("clasificacion")]
        public async Task<IActionResult> ClasificacionUsuarios()
        {
            var usuariosTop = await _usuarioService.ClasificacionUsuarios();
            return Ok(usuariosTop);
        }



       // GET: api/Usuario/5
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

       // POST: api/Usuario
       [HttpPost]
       public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
       {
           await _usuarioService.AddAsync(usuario);
           return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
       }

       // PUT: api/Usuario/5
       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateUsuario(int id, Usuario updatedUsuario)
       {
           var existingUsuario = await _usuarioService.GetByIdAsync(id);
           if (existingUsuario == null)
           {
               return NotFound();
           }

           existingUsuario.Nombre = updatedUsuario.Nombre;
           existingUsuario.Apellido = updatedUsuario.Apellido;
           existingUsuario.Gmail = updatedUsuario.Gmail;
           existingUsuario.Telefono = updatedUsuario.Telefono;
           existingUsuario.Contraseña = updatedUsuario.Contraseña;
           existingUsuario.IdRol = updatedUsuario.IdRol;

           await _usuarioService.UpdateAsync(existingUsuario);
           return NoContent();
       }

       // DELETE: api/Usuario/5
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

       // POST: api/Usuario/login
       [HttpPost("login")]
       public async Task<ActionResult<Usuario>> Login([FromBody] LoginDto loginDto)
       {
           var usuario = await _usuarioService.LoginAsync(loginDto.Email, loginDto.Password);
           
           if (usuario == null)
           {
               return Unauthorized(new { message = "Credenciales inválidas" });
           }
           
           return Ok(usuario);
       }

       // POST: api/Usuario/verificar-email
       [HttpGet("verificar-email")]
       public async Task<ActionResult<bool>> VerificarEmail([FromQuery] string email)
       {
           var usuarios = await _usuarioService.GetAllUsuariosAsync();
           var existe = usuarios.Any(u => u.Gmail == email);
           return Ok(existe);
       }
   }

   // DTO para login
   public class LoginDto
   {
       public string Email { get; set; }
       public string Password { get; set; }
   }
}