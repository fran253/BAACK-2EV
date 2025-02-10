using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly string _connectionString;

        public ComentarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Comentario>> GetAllAsync()
        {
            var comentarios = new List<Comentario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdComentario, Contenido, FechaCreacion, IdUsuario, IdArchivo FROM Comentario";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var comentario = new Comentario
                            {
                                IdComentario = reader.GetInt32(0),
                                Contenido = reader.GetString(1),
                                FechaCreacion = reader.GetDateTime(4),
                                IdUsuario = reader.GetInt32(5),
                                IdArchivo = reader.GetInt32(6)

                            };

                            comentarios.Add(comentario);
                        }
                    }
                }
            }
            return comentarios;
        }

        public async Task<Comentario> GetByIdAsync(int id)
        {
            Comentario comentario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdComentario, Contenido, FechaCreacion, IdUsuario, IdArchivo FROM Comentario WHERE IdComentario = @IdComentario";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdComentario", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            comentario = new Comentario
                            {
                                IdComentario = reader.GetInt32(0),
                                Contenido = reader.GetString(1),
                                FechaCreacion = reader.GetDateTime(4),
                                IdUsuario = reader.GetInt32(5),
                                IdArchivo = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
            return comentario;
        }

        public async Task AddAsync(Comentario comentario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Comentario (Contenido, FechaCreacion, IdUsuario, IdArchivo) VALUES (@Contenido, @FechaCreacion, @IdUsuario, @IdArchivo)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Contenido", comentario.Contenido);
                    command.Parameters.AddWithValue("@FechaCreacion", comentario.FechaCreacion);
                    command.Parameters.AddWithValue("@IdUsuario", comentario.IdUsuario);
                    command.Parameters.AddWithValue("@IdArchivo", comentario.IdArchivo);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Comentario comentario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Comentario SET Contenido = @Contenido, FechaCreacion = @FechaCreacion, IdUsuario = @IdUsuario, IdArchivo = @IdArchivo WHERE IdComentario = @IdComentario";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdComentario", comentario.IdArchivo);
                    command.Parameters.AddWithValue("@Contenido", comentario.Contenido);
                    command.Parameters.AddWithValue("@FechaCreacion", comentario.FechaCreacion);
                    command.Parameters.AddWithValue("@IdUsuario", comentario.IdUsuario);
                    command.Parameters.AddWithValue("@IdArchivo", comentario.IdArchivo);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Comentario WHERE IdComentario = @IdComentario";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdComentario", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
