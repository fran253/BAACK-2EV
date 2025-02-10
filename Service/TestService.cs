using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;
using reto2_api.Service;

namespace reto2_api.Service
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<List<Test>> GetAllAsync()
        {
            return await _testRepository.GetAllAsync();
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            return await _testRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Test test)
        {
            await _testRepository.AddAsync(test);
        }

        public async Task UpdateAsync(Test test)
        {
            await _testRepository.UpdateAsync(test);
        }
        public async Task DeleteAsync(int id)
        {
           var test = await _testRepository.GetByIdAsync(id);
           if (test == null)
           {
               throw KeyNotFoundException("Test no encontrado");
           }
           await _testRepository.DeleteAsync(id);
        }

        private Exception KeyNotFoundException(string v)
        {
            throw new NotImplementedException();
        }

        ///METODO TEST DE UN TEMA
        public async Task<List<Test>> GetByTemarioIdAsync(int idTemario)
        {
            if (idTemario <= 0)
                throw new ArgumentException("el id del temario debe ser un número positivo.", nameof(idTemario));

            return await _testRepository.GetByTemarioIdAsync(idTemario);
        }
        
    }
}


