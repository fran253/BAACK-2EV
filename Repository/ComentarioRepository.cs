using MySql.Data.MySqlClient;
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

    using (var connection = new MySqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        string query = "SELECT idComentario, contenido, fechaCreacion, idUsuario, idArchivo FROM Comentario"; // ✅ Se cambió idArchivo por idTemario
        
        using (var command = new MySqlCommand(query, connection))
        {
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var comentario = new Comentario
                    {
                        IdComentario = reader.GetInt32(0),
                        Contenido = reader.GetString(1),
                        FechaCreacion = reader.IsDBNull(2) ? DateTime.UtcNow : reader.GetDateTime(2),
                        IdUsuario = reader.GetInt32(3),
                        IdArchivo = reader.GetInt32(4)
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

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idComentario, contenido, fechaCreacion, idUsuario, idArchivo FROM Comentario WHERE idComentario = @IdComentario";
                using (var command = new MySqlCommand(query, connection))
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
                            FechaCreacion = reader.IsDBNull(2) ? DateTime.UtcNow : reader.GetDateTime(2),
                            IdUsuario = reader.GetInt32(3),
                            IdArchivo = reader.GetInt32(4)
                            };
                        }
                    }
                }
            }
            return comentario;
        }

        public async Task AddAsync(Comentario comentario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Comentario (contenido, fechaCreacion, idUsuario, idArchivo) VALUES (@Contenido, @FechaCreacion, @IdUsuario, @IdArchivo)";
                using (var command = new MySqlCommand(query, connection))
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
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Comentario SET contenido = @Contenido, fechaCreacion = @FechaCreacion, idUsuario = @IdUsuario, idArchivo = @IdArchivo WHERE idComentario = @IdComentario";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdComentario", comentario.IdComentario);
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
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Comentario WHERE idComentario = @IdComentario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdComentario", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<Comentario>> GetByArchivoIdAsync(int idArchivo)
        {
            var comentarios = new List<Comentario>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idComentario, contenido, fechaCreacion, idUsuario, idArchivo FROM Comentario WHERE idArchivo = @IdArchivo ORDER BY fechaCreacion DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdArchivo", idArchivo);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var comentario = new Comentario
                            {
                                IdComentario = reader.GetInt32(0),
                                Contenido = reader.GetString(1),
                                FechaCreacion = reader.IsDBNull(2) ? DateTime.UtcNow : reader.GetDateTime(2),
                                IdUsuario = reader.GetInt32(3),
                                IdArchivo = reader.GetInt32(4)
                            };

                            comentarios.Add(comentario);
                        }
                    }
                }
            }
            return comentarios;
        }

    }
}
