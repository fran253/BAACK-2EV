namespace reto2_api.Repositories
{
    public interface IPreguntaRepository
    {
        Task<List<Pregunta>> GetAllAsync();
        Task<Pregunta?> GetByIdAsync(int id);
        Task AddAsync(Pregunta pregunta);
        Task UpdateAsync(Pregunta pregunta);
        Task<bool> DeleteAsync(int id);
    }
}
