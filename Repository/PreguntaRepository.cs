using MySql.Data.MySqlClient;
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

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idPregunta, enunciado, idTest FROM Pregunta";
                using (var command = new MySqlCommand(query, connection))
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

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idPregunta, enunciado, idTest FROM Pregunta WHERE idPregunta = @IdPregunta";
                using (var command = new MySqlCommand(query, connection))
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
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Pregunta (enunciado, idTest) VALUES (@Enunciado, @IdTest)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Enunciado", pregunta.Enunciado);
                    command.Parameters.AddWithValue("@IdTest", pregunta.Test.IdTest);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Pregunta pregunta)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Pregunta SET enunciado = @Enunciado, idTest = @IdTest WHERE idPregunta = @IdPregunta";
                using (var command = new MySqlCommand(query, connection))
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
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Pregunta WHERE idPregunta = @IdPregunta";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPregunta", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        ///METODO PREGUNTAS DE UN TEST
        public async Task<List<Pregunta>> GetByTestIdAsync(int idTest)
        {
            var preguntas = new List<Pregunta>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idPregunta, enunciado FROM Pregunta WHERE idTest = @IdTest";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTest", idTest);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            preguntas.Add(new Pregunta
                            {
                                IdPregunta = reader.GetInt32(0),
                                Enunciado = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return preguntas;
        }
    }
}
