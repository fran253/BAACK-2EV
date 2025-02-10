

namespace reto2_api.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int idUsuario);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int idUsuario);
        Task InicializarDatosAsync();
    }
}
