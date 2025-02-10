namespace reto2_api.Repositories
{
    public interface IAsignaturaRepository
    {
        Task<List<Asignatura>> GetAllAsync();
        Task<Asignatura?> GetByIdAsync(int id);
        Task<Asignatura?> GetByCursoIdAsync(int idCurso);
        Task AddAsync(Asignatura asignatura);
        Task UpdateAsync(Asignatura asignatura);
        Task<bool> DeleteAsync(int id);
    }
}
