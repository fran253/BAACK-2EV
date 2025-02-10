
namespace reto2_api.Service
{
    public interface IArchivoService
    {
        Task<List<Archivo>> GetAllAsync();
        Task<Archivo?> GetByIdAsync(int id);
        Task AddAsync(Archivo archivo);
        Task UpdateAsync(Archivo archivo);
        Task DeleteAsync(int id);
    }
}