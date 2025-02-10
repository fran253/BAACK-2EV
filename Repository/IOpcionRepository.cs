namespace reto2_api.Repositories
{
    public interface IOpcionRepository
    {
        Task<List<Opcion>> GetAllAsync();
        Task<Opcion?> GetByIdAsync(int id);
        Task AddAsync(Opcion opcion);
        Task UpdateAsync(Opcion opcion);
        Task<bool> DeleteAsync(int id);
    }
}
