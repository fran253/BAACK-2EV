namespace reto2_api.Service
{
    public interface IArchivoUsuarioService
    {
        Task<List<int>> GetArchivosGuardadosPorUsuarioAsync(int idUsuario);
        Task AddAsync(ArchivoUsuario archivoUsuario);
        Task DeleteAsync(int idUsuario, int idArchivo);
    }
}
