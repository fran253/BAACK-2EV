using reto2_api.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace reto2_api.Service
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        // Obtener todos los roles
        public async Task<List<Rol>> GetAllRolesAsync()
        {
            return await _rolRepository.GetAllAsync();
        }

        // Obtener un rol por su ID
        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _rolRepository.GetByIdAsync(id);
        }

        // Agregar un nuevo rol
        public async Task AddAsync(Rol rol)
        {
            await _rolRepository.AddAsync(rol);
        }

        // Actualizar un rol existente
        public async Task UpdateAsync(Rol rol)
        {
            await _rolRepository.UpdateAsync(rol);
        }

        // Eliminar un rol
        public async Task DeleteAsync(int id)
        {
            var rol = await _rolRepository.GetByIdAsync(id);
            if (rol == null)
            {
                throw new KeyNotFoundException("rol no encontrado");
            }
            await _rolRepository.DeleteAsync(id);
        }
    }
}
