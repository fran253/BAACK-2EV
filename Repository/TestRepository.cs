using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly string _connectionString;

        public TestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Test>> GetAllAsync()
        {
            var tests = new List<Test>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdTest, Titulo, FechaCreacion, IdTemario FROM Test";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var test = new Test
                            {
                                IdTest = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                FechaCreacion = reader.GetDateTime(2),
                                IdTemario = reader.GetInt32(3)
                            };

                            tests.Add(test);
                        }
                    }
                }
            }
            return tests;
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            Test test = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdTest, Titulo, FechaCreacion, IdTemario FROM Test WHERE IdTest = @IdTest";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTest", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            test = new Test
                            {
                                IdTest = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                FechaCreacion = reader.GetDateTime(2),
                                IdTemario = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return test;
        }

        public async Task AddAsync(Test test)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Test (Titulo, FechaCreacion, IdTemario) VALUES (@Titulo, @FechaCreacion, @IdTemario)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", test.Titulo);
                    command.Parameters.AddWithValue("@FechaCreacion", test.FechaCreacion);
                    command.Parameters.AddWithValue("@IdTemario", test.IdTemario);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Test test)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Test SET Titulo = @Titulo, FechaCreacion = @FechaCreacion, IdTemario = @IdTemario WHERE IdTest = @IdTest";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTest", test.IdTest);
                    command.Parameters.AddWithValue("@Titulo", test.Titulo);
                    command.Parameters.AddWithValue("@FechaCreacion", test.FechaCreacion);
                    command.Parameters.AddWithValue("@IdTemario", test.IdTemario);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
       
        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Test WHERE IdTest = @IdTest";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTest", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        ///METODO TEST DE UN TEMA
         public async Task<List<Test>> GetByTemarioIdAsync(int idTemario)
        {
            var tests = new List<Test>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdTest, Titulo, FechaCreacion FROM Test WHERE IdTemario = @IdTemario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTemario", idTemario);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tests.Add(new Test
                            {
                                IdTest = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                FechaCreacion = reader.GetDateTime(2)
                            });
                        }
                    }
                }
            }

            return tests;
        }
    }
}
