using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;

namespace reto2_api.Service
{
    public class UsuarioCursoService : IUsuarioCursoService
    {
        private readonly IUsuarioCursoRepository _usuarioCursoRepository;

        public UsuarioCursoService(IUsuarioCursoRepository usuarioCursoRepository)
        {
            _usuarioCursoRepository = usuarioCursoRepository;
        }

        public async Task<List<UsuarioCurso>> GetAllAsync()
        {
            return await _usuarioCursoRepository.GetAllAsync();
        }

        public async Task<List<UsuarioCurso>> GetByUsuarioIdAsync(int idUsuario)
        {
            return await _usuarioCursoRepository.GetByUsuarioIdAsync(idUsuario);
        }

        public async Task<List<UsuarioCurso>> GetByCursoIdAsync(int idCurso)
        {
            return await _usuarioCursoRepository.GetByCursoIdAsync(idCurso);
        }

        public async Task AddAsync(UsuarioCurso usuarioCurso)
        {
            if (usuarioCurso == null)
                throw new ArgumentNullException(nameof(usuarioCurso), "la inscripción no puede ser nula.");

            if (usuarioCurso.IdUsuario <= 0 || usuarioCurso.IdCurso <= 0)
                throw new ArgumentException("los ids de usuario y curso deben ser números positivos.");

            await _usuarioCursoRepository.AddAsync(usuarioCurso);
        }

        public async Task DeleteAsync(int idUsuario, int idCurso)
        {
            await _usuarioCursoRepository.DeleteAsync(idUsuario, idCurso);
        }
    }
}
