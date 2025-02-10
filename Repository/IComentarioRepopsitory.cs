namespace reto2_api.Repositories
{
    public interface IComentarioRepository
    {
        Task<List<Comentario>> GetAllAsync();
        Task<Comentario?> GetByIdAsync(int id);
        Task AddAsync(Comentario comentario);
        Task UpdateAsync(Comentario comentario);
        Task<bool> DeleteAsync(int id);
    }
}
