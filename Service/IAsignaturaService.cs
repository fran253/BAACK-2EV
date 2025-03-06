
namespace reto2_api.Service
{
    public interface IAsignaturaService
    {
        Task<List<Asignatura>> GetAllAsync();
        Task<Asignatura?> GetByIdAsync(int id);
        Task<List<Asignatura>> GetByIdCursoAsync(int idCurso); ///METODO PARA OBTENER ASIGNATURAS POR ID DE CURSO
        Task AddAsync(Asignatura asignatura);
        Task UpdateAsync(Asignatura asignatura);
        Task DeleteAsync(int id);
    }
}