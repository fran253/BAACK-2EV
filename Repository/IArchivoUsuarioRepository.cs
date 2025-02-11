namespace reto2_api.Repositories
{
    public interface IArchivoUsuarioRepository
    {
        Task<List<int>> GetArchivosGuardadosPorUsuarioAsync(int idUsuario);
        Task AddAsync(ArchivoUsuario archivoUsuario);
        Task DeleteAsync(int idUsuario, int idArchivo);
    }
}
