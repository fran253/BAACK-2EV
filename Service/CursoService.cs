using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Service
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task<List<Curso>> GetAllAsync()
        {
            return await _cursoRepository.GetAllAsync();
        }

        public async Task<Curso?> GetByIdAsync(int id)
        {
            return await _cursoRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Curso curso)
        {
            await _cursoRepository.AddAsync(curso);
        }

        public async Task UpdateAsync(Curso curso)
        {
            await _cursoRepository.UpdateAsync(curso);
        }

        public async Task DeleteAsync(int id)
        {
           var curso = await _cursoRepository.GetByIdAsync(id);
           if (curso == null)
           {
               throw KeyNotFoundException("Curso no encontrado");
           }
           await _cursoRepository.DeleteAsync(id);
        }

        private Exception KeyNotFoundException(string v)
        {
            throw new NotImplementedException();
        }

        
    }
}


