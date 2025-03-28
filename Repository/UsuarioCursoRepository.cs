using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class UsuarioCursoRepository : IUsuarioCursoRepository
    {
        private readonly string _connectionString;

        public UsuarioCursoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<UsuarioCurso>> GetAllAsync()
        {
            var inscripciones = new List<UsuarioCurso>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idUsuario, idCurso FROM Usuario_Curso";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            inscripciones.Add(new UsuarioCurso
                            {
                                IdUsuario = reader.GetInt32(0),
                                IdCurso = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            return inscripciones;
        }

        public async Task<List<UsuarioCurso>> GetByUsuarioIdAsync(int idUsuario)
        {
            var inscripciones = new List<UsuarioCurso>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdUsuario, IdCurso FROM Usuario_Curso WHERE IdUsuario = @IdUsuario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            inscripciones.Add(new UsuarioCurso
                            {
                                IdUsuario = reader.GetInt32(0),
                                IdCurso = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            return inscripciones;
        }

        public async Task<List<UsuarioCurso>> GetByIdCursoAsync(int idCurso)
        {
            var inscripciones = new List<UsuarioCurso>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdUsuario, IdCurso FROM Usuario_Curso WHERE IdCurso = @IdCurso";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCurso", idCurso);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            inscripciones.Add(new UsuarioCurso
                            {
                                IdUsuario = reader.GetInt32(0),
                                IdCurso = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            return inscripciones;
        }

        public async Task AddAsync(UsuarioCurso usuarioCurso)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Usuario_Curso (IdUsuario, IdCurso) VALUES (@IdUsuario, @IdCurso)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", usuarioCurso.IdUsuario);
                    command.Parameters.AddWithValue("@IdCurso", usuarioCurso.IdCurso);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int idUsuario, int idCurso)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Usuario_Curso WHERE IdUsuario = @IdUsuario AND IdCurso = @IdCurso";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@IdCurso", idCurso);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
