using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class ArchivoUsuarioRepository : IArchivoUsuarioRepository
    {
        private readonly string _connectionString;

        public ArchivoUsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<int>> GetArchivosGuardadosPorUsuarioAsync(int idUsuario)
        {
            var archivos = new List<int>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdArchivo FROM ArchivoUsuario WHERE IdUsuario = @IdUsuario";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            archivos.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return archivos;
        }

        public async Task AddAsync(ArchivoUsuario archivoUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO ArchivoUsuario (IdUsuario, IdArchivo, FechaGuardado) VALUES (@IdUsuario, @IdArchivo, @FechaGuardado)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", archivoUsuario.IdUsuario);
                    command.Parameters.AddWithValue("@IdArchivo", archivoUsuario.IdArchivo);
                    command.Parameters.AddWithValue("@FechaGuardado", archivoUsuario.FechaGuardado);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int idUsuario, int idArchivo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM ArchivoUsuario WHERE IdUsuario = @IdUsuario AND IdArchivo = @IdArchivo";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@IdArchivo", idArchivo);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
