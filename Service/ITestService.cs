
namespace reto2_api.Service
{
    public interface ITestService
    {
        Task<List<Test>> GetAllAsync();
        Task<Test?> GetByIdAsync(int id);
        Task<List<Test>> GetByTemarioIdAsync(int idTemario); ///METODO TEST DE UN TEMA
        Task AddAsync(Test test);
        Task UpdateAsync(Test test);
        Task DeleteAsync(int id);
    }

}