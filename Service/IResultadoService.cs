namespace reto2_api.Service
{
    public interface IResultadoService
    {
        Task<List<Resultado>> GetAllAsync();
        Task<Resultado?> GetByIdAsync(int id);
        Task<int> GetResultadoFinalPorUsuarioTestAsync(int idUsuario, int idTest);///METODO RESILTADO DE USUARIO
        Task AddAsync(Resultado resultado);
        Task UpdateAsync(Resultado resultado);
        Task DeleteAsync(int id);
    }
}
