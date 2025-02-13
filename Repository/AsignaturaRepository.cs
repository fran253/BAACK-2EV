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
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Imagen = reader.IsDBNull(3) ? null : reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4),
                                IdCurso = reader.GetInt32(5)
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

                string query = "SELECT idAsignatura, nombre, descripcion, imagen, fechaCreacion, idCurso FROM Asignatura WHERE idAsignatura = @IdAsignatura";
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
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Imagen = reader.IsDBNull(3) ? null : reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4),
                                IdCurso = reader.GetInt32(5)
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

                string query = "INSERT INTO Asignatura (Nombre, Descripcion, Imagen, FechaCreacion, IdCurso) VALUES (@Nombre, @Descripcion, @Imagen, @FechaCreacion, @IdCurso)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", asignatura.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", asignatura.Descripcion);
                    command.Parameters.AddWithValue("@Imagen", asignatura.Imagen);
                    command.Parameters.AddWithValue("@FechaCreacion", asignatura.FechaCreacion);
                    command.Parameters.AddWithValue("@IdCurso", asignatura.IdCurso);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

public async Task UpdateAsync(Asignatura asignatura)
{
    using (var connection = new MySqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        // 🔹 Obtener los valores actuales de la asignatura
        string selectQuery = "SELECT Nombre, Descripcion, Imagen, FechaCreacion, IdCurso FROM Asignatura WHERE IdAsignatura = @IdAsignatura";
        Asignatura asignaturaActual = null;

        using (var selectCommand = new MySqlCommand(selectQuery, connection))
        {
            selectCommand.Parameters.AddWithValue("@IdAsignatura", asignatura.IdAsignatura);

            using (var reader = await selectCommand.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    asignaturaActual = new Asignatura
                    {
                        IdAsignatura = asignatura.IdAsignatura,
                        Nombre = reader.GetString(0),
                        Descripcion = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Imagen = reader.IsDBNull(2) ? null : reader.GetString(2),
                        FechaCreacion = reader.GetDateTime(3),
                        IdCurso = reader.GetInt32(4)
                    };
                }
            }
        }

        if (asignaturaActual == null)
            throw new Exception("La asignatura no existe.");

        string query = @"
            UPDATE Asignatura 
            SET 
                Nombre = @Nombre, 
                Descripcion = @Descripcion, 
                Imagen = @Imagen, 
                IdCurso = @IdCurso
            WHERE IdAsignatura = @IdAsignatura";

        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@IdAsignatura", asignatura.IdAsignatura);
            command.Parameters.AddWithValue("@Nombre", asignatura.Nombre ?? asignaturaActual.Nombre);
            command.Parameters.AddWithValue("@Descripcion", asignatura.Descripcion ?? asignaturaActual.Descripcion);
            command.Parameters.AddWithValue("@Imagen", asignatura.Imagen ?? asignaturaActual.Imagen);
            command.Parameters.AddWithValue("@IdCurso", asignatura.IdCurso > 0 ? asignatura.IdCurso : asignaturaActual.IdCurso);

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
        public async Task<List<Asignatura>> GetByIdCursoAsync(int idCurso)
        {
            var asignaturas = new List<Asignatura>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idAsignatura, nombre, descripcion, imagen, fechaCreacion FROM Asignatura WHERE idCurso = @IdCurso";
                
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
                                Nombre = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Imagen = reader.IsDBNull(3) ? null : reader.GetString(3),
                                FechaCreacion = reader.IsDBNull(4) ? DateTime.UtcNow : reader.GetDateTime(4)
                            });
                        }
                    }
                }
            }

            return asignaturas; // Devuelve una lista, no un solo objeto
        }


        
    }
}
