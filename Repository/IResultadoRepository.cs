namespace reto2_api.Repositories
{
    public interface IResultadoRepository
    {
        Task<List<Resultado>> GetAllAsync();
        Task<Resultado?> GetByIdAsync(int id);
        Task AddAsync(Resultado resultado);
        Task UpdateAsync(Resultado resultado);
        Task<bool> DeleteAsync(int id);
    }
}
