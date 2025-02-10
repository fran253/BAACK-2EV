namespace reto2_api.Service
{
    public interface IUsuarioCursoService
    {
        Task<List<UsuarioCurso>> GetAllAsync();
        Task<List<UsuarioCurso>> GetByUsuarioIdAsync(int idUsuario);
        Task<List<UsuarioCurso>> GetByCursoIdAsync(int idCurso);
        Task AddAsync(UsuarioCurso usuarioCurso);
        Task DeleteAsync(int idUsuario, int idCurso);
    }
}
