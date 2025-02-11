using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class ResultadoRepository : IResultadoRepository
    {
        private readonly string _connectionString;

        public ResultadoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Resultado>> GetAllAsync()
        {
            var resultados = new List<Resultado>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdResultado, Puntuacion, Fecha, IdUsuario, IdPregunta FROM Resultado";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var resultado = new Resultado
                            {
                                IdResultado = reader.GetInt32(0),
                                Puntuacion = reader.GetInt32(1),
                                Fecha = reader.GetDateTime(2),
                                Usuario = new Usuario { IdUsuario = reader.GetInt32(3) },
                                Pregunta = new Pregunta { IdPregunta = reader.GetInt32(4) }
                            };

                            resultados.Add(resultado);
                        }
                    }
                }
            }
            return resultados;
        }

        public async Task<Resultado?> GetByIdAsync(int id)
        {
            Resultado? resultado = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdResultado, Puntuacion, Fecha, IdUsuario, IdPregunta FROM Resultado WHERE IdResultado = @IdResultado";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdResultado", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            resultado = new Resultado
                            {
                                IdResultado = reader.GetInt32(0),
                                Puntuacion = reader.GetInt32(1),
                                Fecha = reader.GetDateTime(2),
                                Usuario = new Usuario { IdUsuario = reader.GetInt32(3) },
                                Pregunta = new Pregunta { IdPregunta = reader.GetInt32(4) }
                            };
                        }
                    }
                }
            }
            return resultado;
        }

        public async Task AddAsync(Resultado resultado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Resultado (Puntuacion, Fecha, IdUsuario, IdPregunta) VALUES (@Puntuacion, @Fecha, @IdUsuario, @IdPregunta)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Puntuacion", resultado.Puntuacion);
                    command.Parameters.AddWithValue("@Fecha", resultado.Fecha);
                    command.Parameters.AddWithValue("@IdUsuario", resultado.IdUsuario);
                    command.Parameters.AddWithValue("@IdPregunta", resultado.IdPregunta);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Resultado resultado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Resultado SET Puntuacion = @Puntuacion, Fecha = @Fecha, IdUsuario = @IdUsuario, IdPregunta = @IdPregunta WHERE IdResultado = @IdResultado";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdResultado", resultado.IdResultado);
                    command.Parameters.AddWithValue("@Puntuacion", resultado.Puntuacion);
                    command.Parameters.AddWithValue("@Fecha", resultado.Fecha);
                    command.Parameters.AddWithValue("@IdUsuario", resultado.IdUsuario);
                    command.Parameters.AddWithValue("@IdPregunta", resultado.IdPregunta);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Resultado WHERE IdResultado = @IdResultado";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdResultado", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        ///METODO RESILTADO DE USUARIO
        public async Task<int> GetResultadoFinalPorUsuarioTestAsync(int idUsuario, int idTest)
        {
            int puntuacionTotal = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT SUM(Puntuacion)
                    FROM Resultado r
                    JOIN Pregunta p ON r.IdPregunta = p.IdPregunta
                    WHERE r.IdUsuario = @IdUsuario AND p.IdTest = @IdTest";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@IdTest", idTest);

                    var result = await command.ExecuteScalarAsync();
                    puntuacionTotal = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }

            return puntuacionTotal;
        }
    }
}
