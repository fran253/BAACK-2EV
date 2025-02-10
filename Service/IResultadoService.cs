namespace reto2_api.Service
{
    public interface IResultadoService
    {
        Task<List<Resultado>> GetAllAsync();
        Task<Resultado?> GetByIdAsync(int id);
        Task AddAsync(Resultado resultado);
        Task UpdateAsync(Resultado resultado);
        Task DeleteAsync(int id);
    }
}
