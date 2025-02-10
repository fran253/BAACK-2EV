namespace reto2_api.Service
{
    public interface IOpcionService
    {
        Task<List<Opcion>> GetAllAsync();
        Task<Opcion?> GetByIdAsync(int id);
        Task<List<Opcion>> GetByPreguntaIdAsync(int idPregunta);///METODO OPCIONES DE PREGUNTA
        Task AddAsync(Opcion opcion);
        Task UpdateAsync(Opcion opcion);
        Task DeleteAsync(int id);
    }
}
