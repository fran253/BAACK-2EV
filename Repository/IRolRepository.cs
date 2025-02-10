
namespace reto2_api.Repositories
{
    public interface IRolRepository
    {
        Task<List<Rol>> GetAllAsync();
        Task<Rol?> GetByIdAsync(int idRol);
        Task AddAsync(Rol rol);
        Task UpdateAsync(Rol rol);
        Task<bool> DeleteAsync(int idRol);
        Task InicializarDatosAsync();
    }
}
