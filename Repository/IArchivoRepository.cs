namespace reto2_api.Repositories
{
    public interface IArchivoRepository
    {
        Task<List<Archivo>> GetAllAsync();
        Task<Archivo?> GetByIdAsync(int id);
        Task AddAsync(Archivo archivo);
        Task UpdateAsync(Archivo archivo);
        Task<bool> DeleteAsync(int id);
    }
}
