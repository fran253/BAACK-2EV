namespace reto2_api.Service
{
    public interface IPreguntaService
    {
        Task<List<Pregunta>> GetAllAsync();
        Task<Pregunta?> GetByIdAsync(int id);
        Task<List<Pregunta>> GetByTestIdAsync(int idTest);///METODO PREGUNTAS DE UN TEST
        Task AddAsync(Pregunta pregunta);
        Task UpdateAsync(Pregunta pregunta);
        Task DeleteAsync(int id);
    }
}
