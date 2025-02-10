
namespace reto2_api.Repositories
{
    public interface ITemarioRepository
    {
        Task<List<Temario>> GetAllAsync();
        Task<Temario?> GetByIdAsync(int id);
        Task<List<Temario>> GetByAsignaturaIdAsync(int idAsignatura); ///METODO PARA OBTENER LOS TEMARIOS POR ASIGNATURA
        Task AddAsync(Temario temario);
        Task UpdateAsync(Temario temario);
        Task<bool> DeleteAsync(int id);
    }
}
