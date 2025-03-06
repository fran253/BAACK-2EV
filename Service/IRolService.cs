using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Service
{
    public interface IRolService
    {
        Task<List<Rol>> GetAllRolesAsync();
        Task<Rol?> GetByIdAsync(int id);
        Task AddAsync(Rol rol);
        Task UpdateAsync(Rol rol);
        Task DeleteAsync(int id);
    }
}
