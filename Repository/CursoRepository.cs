
using Microsoft.Data.SqlClient;


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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idCurso, nombre, descripcion, imagen, fechaCreacion FROM Curso";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var curso = new Curso
                            {
                                IdCurso = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Imagen = reader.GetString(3),
                                FechaCreacion = reader.GetDateTime(4)
                            }; 

                            cursos.Add(curso);
                        }
                    }
                }
            }
            return cursos;
        }

        public async Task<Curso> GetByIdAsync(int id)
        {
            Curso curso = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idCurso, nombre, descripcion, imagen, fechaCreacion FROM Curso WHERE IdCurso = @IdCurso";
                using (var command = new SqlCommand(query, connection))
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
                                Descripcion = reader.GetString(1),
                                Imagen = reader.GetString(3),
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
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Curso (nombre, descripcion, imagen) VALUES (@nombre, @descripcion, @imagen)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", curso.Nombre);
                    command.Parameters.AddWithValue("@descripcion", curso.Descripcion);
                    command.Parameters.AddWithValue("@imagen", curso.Imagen);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Curso curso)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Curso SET Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen WHERE IdCurso = @IdCurso";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCurso", curso.IdCurso);
                    command.Parameters.AddWithValue("@Nombre", curso.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", curso.Descripcion);
                    command.Parameters.AddWithValue("@Imagen", curso.Imagen);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Curso WHERE IdCurso = @IdCurso";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCurso", id);

                   int rowsAffected = await command.ExecuteNonQueryAsync();
                   return rowsAffected > 0;
                }
            }
        }




    }

}