using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class PreguntaRepository : IPreguntaRepository
    {
        private readonly string _connectionString;

        public PreguntaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Pregunta>> GetAllAsync()
        {
            var preguntas = new List<Pregunta>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdPregunta, Enunciado, IdTest FROM Pregunta";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pregunta = new Pregunta
                            {
                                IdPregunta = reader.GetInt32(0),
                                Enunciado = reader.GetString(1),
                                Test = new Test { IdTest = reader.GetInt32(2) } // Relación con Test
                            };

                            preguntas.Add(pregunta);
                        }
                    }
                }
            }
            return preguntas;
        }

        public async Task<Pregunta?> GetByIdAsync(int id)
        {
            Pregunta pregunta = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdPregunta, Enunciado, IdTest FROM Pregunta WHERE IdPregunta = @IdPregunta";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPregunta", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            pregunta = new Pregunta
                            {
                                IdPregunta = reader.GetInt32(0),
                                Enunciado = reader.GetString(1),
                                Test = new Test { IdTest = reader.GetInt32(2) } // Relación con Test
                            };
                        }
                    }
                }
            }
            return pregunta;
        }

        public async Task AddAsync(Pregunta pregunta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Pregunta (Enunciado, IdTest) VALUES (@Enunciado, @IdTest)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Enunciado", pregunta.Enunciado);
                    command.Parameters.AddWithValue("@IdTest", pregunta.Test.IdTest);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Pregunta pregunta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Pregunta SET Enunciado = @Enunciado, IdTest = @IdTest WHERE IdPregunta = @IdPregunta";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPregunta", pregunta.IdPregunta);
                    command.Parameters.AddWithValue("@Enunciado", pregunta.Enunciado);
                    command.Parameters.AddWithValue("@IdTest", pregunta.Test.IdTest);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Pregunta WHERE IdPregunta = @IdPregunta";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPregunta", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
