using Microsoft.Data.SqlClient;
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdTemario, Titulo, Descripcion, FechaPublicacion, AsignaturaId FROM Temario";
                using (var command = new SqlCommand(query, connection))
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdTemario, Titulo, Descripcion, IdAsignatura FROM Temario WHERE IdTemario = @IdTemario";
                using (var command = new SqlCommand(query, connection))
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
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Temario (Titulo, Descripcion, FechaPublicacion, IdAsignatura) VALUES (@Titulo, @Descripcion, @IdAsignatura)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", temario.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", temario.Descripcion);
                    command.Parameters.AddWithValue("@AsignaturaId", temario.IdAsignatura);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Temario temario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Temario SET Titulo = @Titulo, Descripcion = @Descripcion, IdAsignatura = @IdAsignatura WHERE IdTemario = @IdTemario";
                using (var command = new SqlCommand(query, connection))
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
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Temario WHERE IdTemario = @IdTemario";
                using (var command = new SqlCommand(query, connection))
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // 🔹 Se añadió `Descripcion` e `IdAsignatura` a la consulta
                string query = "SELECT IdTemario, Titulo, Descripcion, IdAsignatura FROM Temario WHERE IdAsignatura = @IdAsignatura";

                using (var command = new SqlCommand(query, connection))
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
