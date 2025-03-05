using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly string _connectionString;

        public ArchivoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Archivo>> GetAllAsync()
        {
            var archivos = new List<Archivo>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idArchivo, titulo, url, tipo, fechaCreacion, idUsuario, idTemario FROM Archivo";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var archivo = new Archivo
                            {
                                IdArchivo = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Url = reader.GetString(2),
                                Tipo = reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4),
                                IdUsuario = reader.GetInt32(5),
                                IdTemario = reader.GetInt32(6)

                            };

                            archivos.Add(archivo);
                        }
                    }
                }
            }
            return archivos;
        }

        public async Task<Archivo> GetByIdAsync(int id)
        {
            Archivo archivo = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idArchivo, titulo, url, tipo, fechaCreacion, idUsuario, idTemario FROM Archivo WHERE idArchivo = @IdArchivo";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdArchivo", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            archivo = new Archivo
                            {
                                IdArchivo = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Url = reader.GetString(2),
                                Tipo = reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4),
                                IdUsuario = reader.GetInt32(5),
                                IdTemario = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
            return archivo;
        }

        public async Task AddAsync(Archivo archivo)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Archivo (titulo, url, tipo, fechaCreacion, idUsuario, idTemario) VALUES (@Titulo, @Url, @Tipo, @FechaCreacion, @IdUsuario, @IdTemario)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", archivo.Titulo);
                    command.Parameters.AddWithValue("@Url", archivo.Url);
                    command.Parameters.AddWithValue("@Tipo", archivo.Tipo);
                    command.Parameters.AddWithValue("@FechaCreacion", archivo.FechaCreacion);
                    command.Parameters.AddWithValue("@IdUsuario", archivo.IdUsuario);
                    command.Parameters.AddWithValue("@IdTemario", archivo.IdTemario);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Archivo archivo)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Archivo SET nombre = @Nombre, descripcion = @Descripcion, imagen = @Imagen, fechaCreacion = @FechaCreacion, idCurso = @IdCurso WHERE idArchivo = @IdArchivo";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdArchivo", archivo.IdArchivo);
                    command.Parameters.AddWithValue("@Titulo", archivo.Titulo);
                    command.Parameters.AddWithValue("@Url", archivo.Url);
                    command.Parameters.AddWithValue("@Tipo", archivo.Tipo);
                    command.Parameters.AddWithValue("@FechaCreacion", archivo.FechaCreacion);
                    command.Parameters.AddWithValue("@IdUsuario", archivo.IdUsuario);
                    command.Parameters.AddWithValue("@IdTemario", archivo.IdTemario);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Archivo WHERE idArchivo = @IdArchivo";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdArchivo", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        ///METODO ARCHIVOS DE UN TEMA
        public async Task<List<Archivo>> GetByTemarioIdAsync(int idTemario)
        {
            var archivos = new List<Archivo>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idArchivo, titulo, url, tipo, fechaCreacion, idUsuario, idTemario FROM Archivo WHERE idTemario = @IdTemario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTemario", idTemario);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            archivos.Add(new Archivo
                            {
                                IdArchivo = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Url = reader.GetString(2),
                                Tipo = reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4),
                                IdUsuario = reader.GetInt32(5),
                                IdTemario = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }

            return archivos;
        }

        public async Task<List<Archivo>> GetByUsuarioIdAsync(int idUsuario)
        {
            var archivos = new List<Archivo>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idArchivo, titulo, url, tipo, fechaCreacion, idUsuario, idTemario FROM Archivo WHERE idUsuario = @idUsuario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            archivos.Add(new Archivo
                            {
                                IdArchivo = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Url = reader.GetString(2),
                                Tipo = reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4),
                                IdUsuario = reader.GetInt32(5),
                                IdTemario = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }

            return archivos;
        }

        
    }
}
