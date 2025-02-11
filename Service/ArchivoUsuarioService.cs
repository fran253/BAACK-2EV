using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;

namespace reto2_api.Service
{
    public class ArchivoUsuarioService : IArchivoUsuarioService
    {
        private readonly IArchivoUsuarioRepository _archivoUsuarioRepository;

        public ArchivoUsuarioService(IArchivoUsuarioRepository archivoUsuarioRepository)
        {
            _archivoUsuarioRepository = archivoUsuarioRepository;
        }

        public async Task<List<int>> GetArchivosGuardadosPorUsuarioAsync(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("el id del usuario debe ser un número positivo.", nameof(idUsuario));

            return await _archivoUsuarioRepository.GetArchivosGuardadosPorUsuarioAsync(idUsuario);
        }

        public async Task AddAsync(ArchivoUsuario archivoUsuario)
        {
            if (archivoUsuario == null)
                throw new ArgumentNullException(nameof(archivoUsuario), "el archivo a guardar no puede ser nulo.");

            await _archivoUsuarioRepository.AddAsync(archivoUsuario);
        }

        public async Task DeleteAsync(int idUsuario, int idArchivo)
        {
            if (idUsuario <= 0 || idArchivo <= 0)
                throw new ArgumentException("los ids deben ser números positivos.");

            await _archivoUsuarioRepository.DeleteAsync(idUsuario, idArchivo);
        }
    }
}
