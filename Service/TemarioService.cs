using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Service
{
    public class TemarioService : ITemarioService
    {
        private readonly ITemarioRepository _temarioRepository;
        public TemarioService(ITemarioRepository temarioRepository)
        {
            _temarioRepository = temarioRepository;
        }

        public async Task<List<Temario>> GetAllAsync()
        {
            return await _temarioRepository.GetAllAsync();
        }

        public async Task<Temario?> GetByIdAsync(int id)
        {
            return await _temarioRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Temario temario)
        {
            await _temarioRepository.AddAsync(temario);
        }

        public async Task UpdateAsync(Temario temario)
        {
            await _temarioRepository.UpdateAsync(temario);
        }

        public async Task DeleteAsync(int id)
        {
           var temario = await _temarioRepository.GetByIdAsync(id);
           if (temario == null)
           {
               throw KeyNotFoundException("Temario no encontrado");
           }
           await _temarioRepository.DeleteAsync(id);
        }

        private Exception KeyNotFoundException(string v)
        {
            throw new NotImplementedException();
        }

        
    }
}


