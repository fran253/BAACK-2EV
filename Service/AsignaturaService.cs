using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;

namespace reto2_api.Service
{
    public class AsignaturaService : IAsignaturaService
    {
        private readonly IAsignaturaRepository _asignaturaRepository;

        public AsignaturaService(IAsignaturaRepository asignaturaRepository)
        {
            _asignaturaRepository = asignaturaRepository;
        }

        public async Task<List<Asignatura>> GetAllAsync()
        {
            return await _asignaturaRepository.GetAllAsync();
        }

        public async Task<Asignatura?> GetByIdAsync(int id)
        {
            return await _asignaturaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Asignatura asignatura)
        {
            await _asignaturaRepository.AddAsync(asignatura);
        }

        public async Task UpdateAsync(Asignatura asignatura)
        {
            await _asignaturaRepository.UpdateAsync(asignatura);
        }

        public async Task DeleteAsync(int id)
        {
            var asignatura = await _asignaturaRepository.GetByIdAsync(id);
            if (asignatura == null)
            {
                throw new KeyNotFoundException("Asignatura no encontrada");
            }
            await _asignaturaRepository.DeleteAsync(id);
        }
        
        ///METODO PARA OBTENER ASIGNATURAS POR ID DE CURSO
        public async Task<List<Asignatura>> GetByIdCursoAsync(int idCurso)
        {
            if (idCurso <= 0)
                throw new ArgumentException("el id del curso debe ser un número positivo.", nameof(idCurso));

            return await _asignaturaRepository.GetByIdCursoAsync(idCurso);
        }
    }
}
