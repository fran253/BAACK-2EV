namespace reto2_api.Repositories
{
    public interface IArchivoRepository
    {
        Task<List<Archivo>> GetAllAsync();
        Task<Archivo?> GetByIdAsync(int id);
        Task<List<Archivo>> GetByTipoAsync(string tipo); //Solo tipo
        Task<List<Archivo>> GetByTipoAndTemarioAsync(string tipo, int idTemario); //Tipo y temario
        Task<List<Archivo>> GetByTemarioIdAsync(int idTemario); ///METODO ARCHIVOS DE UN TEMA
        Task<List<Archivo>> GetByUsuarioIdAsync(int idUsuario); ///METODO ARCHIVOS DE UN USUARIO
        Task AddAsync(Archivo archivo);
        Task UpdateAsync(Archivo archivo);
        Task<bool> DeleteAsync(int id);
    }
}
