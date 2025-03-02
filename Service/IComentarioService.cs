
namespace reto2_api.Service
{
    public interface IComentarioService
    {
        Task<List<Comentario>> GetAllAsync();
        Task<Comentario?> GetByIdAsync(int id);
        Task AddAsync(Comentario comentario);
        Task UpdateAsync(Comentario comentario);
        Task DeleteAsync(int id);
        Task<List<Comentario>> GetByArchivoIdAsync(int idArchivo);
    }
}