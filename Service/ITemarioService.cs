
namespace reto2_api.Service
{
    public interface ITemarioService
    {
        Task<List<Temario>> GetAllAsync();
        Task<Temario?> GetByIdAsync(int id);
        Task<List<Temario>> GetByAsignaturaIdAsync(int idAsignatura); ///METODO PARA OBTENER LOS TEMARIOS POR ASIGNATURA
        Task AddAsync(Temario temario);
        Task UpdateAsync(Temario temario);
        Task DeleteAsync(int id);
    }
}