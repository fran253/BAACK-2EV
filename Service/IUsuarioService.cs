using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Service
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllUsuariosAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
    }
}
