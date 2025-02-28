using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("usuario no encontrado");
            }
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<Usuario?> LoginAsync(string gmail, string contraseña)
        {
            return await _usuarioRepository.LoginByGmailAsync(gmail, contraseña);
        }


    }
}
