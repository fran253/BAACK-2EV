using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Service
{
    public interface ICursoService
    {
        Task<List<Curso>> GetAllAsync();
    }
}
