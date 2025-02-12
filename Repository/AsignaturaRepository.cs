using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class AsignaturaRepository : IAsignaturaRepository
    {
        private readonly string _connectionString;

        public AsignaturaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Asignatura>> GetAllAsync()
        {
            var asignaturas = new List<Asignatura>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idAsignatura, nombre, descripcion, imagen, fechaCreacion, idCurso FROM Asignatura";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var asignatura = new Asignatura
                            {
                                IdAsignatura = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Imagen = reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4),
                                CursoId = reader.GetInt32(5)
                            };

                            asignaturas.Add(asignatura);
                        }
                    }
                }
            }
            return asignaturas;
        }

        public async Task<Asignatura> GetByIdAsync(int id)
        {
            Asignatura asignatura = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdAsignatura, Nombre, Descripcion, Imagen, FechaCreacion, CursoId FROM Asignatura WHERE IdAsignatura = @IdAsignatura";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAsignatura", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            asignatura = new Asignatura
                            {
                                IdAsignatura = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Imagen = reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4),
                                CursoId = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return asignatura;
        }

        public async Task AddAsync(Asignatura asignatura)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Asignatura (Nombre, Descripcion, Imagen, FechaCreacion, CursoId) VALUES (@Nombre, @Descripcion, @Imagen, @FechaCreacion, @CursoId)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", asignatura.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", asignatura.Descripcion);
                    command.Parameters.AddWithValue("@Imagen", asignatura.Imagen);
                    command.Parameters.AddWithValue("@FechaCreacion", asignatura.FechaCreacion);
                    command.Parameters.AddWithValue("@CursoId", asignatura.CursoId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Asignatura asignatura)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Asignatura SET Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen, FechaCreacion = @FechaCreacion, CursoId = @CursoId WHERE IdAsignatura = @IdAsignatura";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAsignatura", asignatura.IdAsignatura);
                    command.Parameters.AddWithValue("@Nombre", asignatura.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", asignatura.Descripcion);
                    command.Parameters.AddWithValue("@Imagen", asignatura.Imagen);
                    command.Parameters.AddWithValue("@FechaCreacion", asignatura.FechaCreacion);
                    command.Parameters.AddWithValue("@CursoId", asignatura.CursoId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Asignatura WHERE IdAsignatura = @IdAsignatura";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAsignatura", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        ///METODO PARA OBTENER LAS ASIGNATURAS POR CURSO
        public async Task<List<Asignatura>> GetByCursoIdAsync(int idCurso)
        {
            var asignaturas = new List<Asignatura>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdAsignatura, Nombre FROM Asignatura WHERE IdCurso = @IdCurso";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCurso", idCurso);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            asignaturas.Add(new Asignatura
                            {
                                IdAsignatura = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return asignaturas; // Devuelve una lista, no un solo objeto
        }

        
    }
}
