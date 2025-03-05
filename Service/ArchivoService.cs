using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;

namespace reto2_api.Service
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _archivoRepository;

        public ArchivoService(IArchivoRepository archivoRepository)
        {
            _archivoRepository = archivoRepository;
        }

        public async Task<List<Archivo>> GetAllAsync()
        {
            return await _archivoRepository.GetAllAsync();
        }

        public async Task<Archivo?> GetByIdAsync(int id)
        {
            return await _archivoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Archivo archivo)
        {
            await _archivoRepository.AddAsync(archivo);
        }

        public async Task UpdateAsync(Archivo archivo)
        {
            await _archivoRepository.UpdateAsync(archivo);
        }

        public async Task DeleteAsync(int id)
        {
            var archivo = await _archivoRepository.GetByIdAsync(id);
            if (archivo == null)
            {
                throw new KeyNotFoundException("Archivo no encontrado");
            }
            await _archivoRepository.DeleteAsync(id);
        }

        ///METODO ARCHIVOS DE UN TEMA
        public async Task<List<Archivo>> GetByTemarioIdAsync(int idTemario)
        {
            if (idTemario <= 0)
                throw new ArgumentException("el id del temario debe ser un número positivo.", nameof(idTemario));

            return await _archivoRepository.GetByTemarioIdAsync(idTemario);
        }

        ///METODO ARCHIVOS DE UN USUARIO
        public async Task<List<Archivo>> GetByUsuarioIdAsync(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("el id del usuario debe ser un número positivo.", nameof(idUsuario));

            return await _archivoRepository.GetByUsuarioIdAsync(idUsuario);
        }
    }
}
