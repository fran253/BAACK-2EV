using MySql.Data.MySqlClient;
using System.Data;

namespace reto2_api.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Curso>> GetAllAsync()
        {
            var cursos = new List<Curso>();

            await  using (var connection = new MySqlConnection(_connectionString))

            try {
            {
                await connection.OpenAsync();

                string query = "SELECT idCurso, nombre, descripcion, imagen, fechaCreacion FROM Curso";
                await using (var command = new MySqlCommand(query, connection))
                {
                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var curso = new Curso
                            {
                                IdCurso = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Imagen = reader.IsDBNull(3) ? null : reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4)
                            };

                            cursos.Add(curso);
                        }
                    }
                }
            }
            }catch(Exception ex){
                return null;

            }         
            finally{  
                if (connection.State != ConnectionState.Closed) {
                    await connection.CloseAsync();
                }
            }
            return cursos;
        }

        public async Task<Curso> GetByIdAsync(int id)
        {
            Curso curso = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idCurso, nombre, descripcion, imagen, fechaCreacion FROM Curso WHERE idCurso = @IdCurso";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCurso", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            curso = new Curso
                            {
                                IdCurso = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Imagen = reader.IsDBNull(3) ? null : reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4)
                            };
                        }
                    }
                }
            }
            return curso;
        }

        public async Task AddAsync(Curso curso)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Curso (nombre, descripcion, imagen) VALUES (@nombre, @descripcion, @imagen)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", curso.Nombre);
                    command.Parameters.AddWithValue("@descripcion", curso.Descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@imagen", curso.Imagen ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Curso curso)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Curso SET nombre = @Nombre, descripcion = @Descripcion, imagen = @Imagen WHERE idCurso = @IdCurso";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCurso", curso.IdCurso);
                    command.Parameters.AddWithValue("@Nombre", curso.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", curso.Descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Imagen", curso.Imagen ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Curso WHERE idCurso = @IdCurso";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCurso", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
