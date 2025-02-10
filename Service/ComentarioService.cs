using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;

namespace reto2_api.Service
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task<List<Comentario>> GetAllAsync()
        {
            return await _comentarioRepository.GetAllAsync();
        }

        public async Task<Comentario?> GetByIdAsync(int id)
        {
            return await _comentarioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Comentario comentario)
        {
            await _comentarioRepository.AddAsync(comentario);
        }

        public async Task UpdateAsync(Comentario comentario)
        {
            await _comentarioRepository.UpdateAsync(comentario);
        }

        public async Task DeleteAsync(int id)
        {
            var comentario = await _comentarioRepository.GetByIdAsync(id);
            if (comentario == null)
            {
                throw new KeyNotFoundException("Comentario no encontrado");
            }
            await _comentarioRepository.DeleteAsync(id);
        }
    }
}
