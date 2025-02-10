
namespace reto2_api.Service
{
    public interface ITemarioService
    {
        Task<List<Temario>> GetAllAsync();
        Task<Temario?> GetByIdAsync(int id);
        Task AddAsync(Temario temario);
        Task UpdateAsync(Temario temario);
        Task DeleteAsync(int id);
    }
}