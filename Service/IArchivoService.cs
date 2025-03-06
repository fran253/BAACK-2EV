
namespace reto2_api.Service
{
    public interface IArchivoService
    {
        Task<List<Archivo>> GetAllAsync();
        Task<Archivo?> GetByIdAsync(int id);
        Task<List<Archivo>> GetByTipoAsync(string tipo);
        Task<List<Archivo>> GetByTipoAndTemarioAsync(string tipo, int idTemario);
        Task<List<Archivo>> GetByTemarioIdAsync(int idTemario);//METODO ARCHIVOS DE UN TEMA
        Task<List<Archivo>> GetByUsuarioIdAsync(int idTemario);//METODO ARCHIVOS DE UN USUARIO
        Task AddAsync(Archivo archivo);
        Task UpdateAsync(Archivo archivo);
        Task DeleteAsync(int id);
    }
}