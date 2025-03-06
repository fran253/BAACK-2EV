using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class TemarioRepository : ITemarioRepository
    {
        private readonly string _connectionString;

        public TemarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Temario>> GetAllAsync()
        {
            var temarios = new List<Temario>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idTemario, titulo, descripcion, idAsignatura FROM Temario";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var temario = new Temario
                            {
                                IdTemario = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                IdAsignatura = reader.GetInt32(3)
                            };

                            temarios.Add(temario);
                        }
                    }
                }
            }
            return temarios;
        }

        public async Task<Temario?> GetByIdAsync(int id)
        {
            Temario temario = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idTemario, titulo, descripcion, idAsignatura FROM Temario WHERE idTemario = @IdTemario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTemario", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            temario = new Temario
                            {
                                IdTemario = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                IdAsignatura = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return temario;
        }

        public async Task AddAsync(Temario temario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Temario (titulo, descripcion, idAsignatura) VALUES (@Titulo, @Descripcion, @IdAsignatura)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", temario.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", temario.Descripcion);
                    command.Parameters.AddWithValue("@IdAsignatura", temario.IdAsignatura);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Temario temario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Temario SET titulo = @Titulo, descripcion = @Descripcion, idAsignatura = @IdAsignatura WHERE idTemario = @IdTemario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTemario", temario.IdTemario);
                    command.Parameters.AddWithValue("@Titulo", temario.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", temario.Descripcion);
                    command.Parameters.AddWithValue("@IdAsignatura", temario.IdAsignatura);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Temario WHERE idTemario = @IdTemario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTemario", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        ///METODO PARA OBTENER LOS TEMARIOS POR ASIGNATURA
        public async Task<List<Temario>> GetByAsignaturaIdAsync(int idAsignatura)
        {
            var temarios = new List<Temario>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // 🔹 Se añadió `Descripcion` e `IdAsignatura` a la consulta
                string query = "SELECT idTemario, titulo, descripcion, idAsignatura FROM Temario WHERE idAsignatura = @IdAsignatura";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAsignatura", idAsignatura);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            temarios.Add(new Temario
                            {
                                IdTemario = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Descripcion = reader.GetString(2),  // 🔹 Ahora se incluye la descripción correctamente
                                IdAsignatura = reader.GetInt32(3)   // 🔹 Se asigna `IdAsignatura` al modelo
                            });
                        }
                    }
                }
            }

            return temarios;
        }
    }
}
