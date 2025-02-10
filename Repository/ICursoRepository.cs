namespace reto2_api.Repositories
{
    public interface ICursoRepository
    {
        Task<List<Curso>> GetAllAsync();
        Task<Curso?> GetByIdAsync(int id);
        Task AddAsync(Curso curso);
        Task UpdateAsync(Curso curso);
        Task<bool> DeleteAsync(int id);
    }
}
