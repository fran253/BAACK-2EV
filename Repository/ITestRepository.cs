
namespace reto2_api.Repositories
{
    public interface ITestRepository
    {
        Task<List<Test>> GetAllAsync();
        Task<Test?> GetByIdAsync(int id);
        Task<List<Test>> GetByTemarioIdAsync(int idTemario); ///METODO TEST DE UN TEMA
        Task AddAsync(Test test);
        Task UpdateAsync(Test test);
        Task<bool> DeleteAsync(int id);
    }
}
