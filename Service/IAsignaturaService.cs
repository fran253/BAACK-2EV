
namespace reto2_api.Service
{
    public interface IAsignaturaService
    {
        Task<List<Asignatura>> GetAllAsync();
        Task<Asignatura?> GetByIdAsync(int id);
        Task AddAsync(Asignatura asignatura);
        Task UpdateAsync(Asignatura asignatura);
        Task DeleteAsync(int id);
    }
}