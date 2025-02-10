namespace reto2_api.Service
{
    public interface IPreguntaService
    {
        Task<List<Pregunta>> GetAllAsync();
        Task<Pregunta?> GetByIdAsync(int id);
        Task AddAsync(Pregunta pregunta);
        Task UpdateAsync(Pregunta pregunta);
        Task DeleteAsync(int id);
    }
}
