using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class OpcionRepository : IOpcionRepository
    {
        private readonly string _connectionString;

        public OpcionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Opcion>> GetAllAsync()
        {
            var opciones = new List<Opcion>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idOpcion, texto, esCorrecta, idPregunta FROM Opcion";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var opcion = new Opcion
                            {
                                IdOpcion = reader.GetInt32(0),
                                Texto = reader.GetString(1),
                                EsCorrecta = reader.GetBoolean(2),
                                Pregunta = new Pregunta { IdPregunta = reader.GetInt32(3) }
                            };

                            opciones.Add(opcion);
                        }
                    }
                }
            }
            return opciones;
        }

        public async Task<Opcion?> GetByIdAsync(int id)
        {
            Opcion? opcion = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idOpcion, texto, esCorrecta, idPregunta FROM Opcion WHERE idOpcion = @IdOpcion";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdOpcion", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            opcion = new Opcion
                            {
                                IdOpcion = reader.GetInt32(0),
                                Texto = reader.GetString(1),
                                EsCorrecta = reader.GetBoolean(2),
                                Pregunta = new Pregunta { IdPregunta = reader.GetInt32(3) }
                            };
                        }
                    }
                }
            }
            return opcion;
        }

        public async Task AddAsync(Opcion opcion)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Opcion (texto, esCorrecta, idPregunta) VALUES (@Texto, @EsCorrecta, @IdPregunta)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Texto", opcion.Texto);
                    command.Parameters.AddWithValue("@EsCorrecta", opcion.EsCorrecta);
                    command.Parameters.AddWithValue("@IdPregunta", opcion.IdPregunta);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Opcion opcion)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Opcion SET texto = @Texto, esCorrecta = @EsCorrecta, idPregunta = @IdPregunta WHERE idOpcion = @IdOpcion";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdOpcion", opcion.IdOpcion);
                    command.Parameters.AddWithValue("@Texto", opcion.Texto);
                    command.Parameters.AddWithValue("@EsCorrecta", opcion.EsCorrecta);
                    command.Parameters.AddWithValue("@IdPregunta", opcion.IdPregunta);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Opcion WHERE idOpcion = @IdOpcion";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdOpcion", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        ///METODO OPCIONES DE PREGUNTA
        public async Task<List<Opcion>> GetByPreguntaIdAsync(int idPregunta)
        {
            var opciones = new List<Opcion>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idOpcion, texto, esCorrecta FROM Opcion WHERE idPregunta = @IdPregunta";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPregunta", idPregunta);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            opciones.Add(new Opcion
                            {
                                IdOpcion = reader.GetInt32(0),
                                Texto = reader.GetString(1),
                                EsCorrecta = reader.GetBoolean(2)
                            });
                        }
                    }
                }
            }

            return opciones;
        }

        ///METODO SOLUCION DE PREGUNTA
        public async Task<Opcion?> GetSolucionByPreguntaIdAsync(int idPregunta)
        {
            Opcion? opcionCorrecta = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idOpcion, texto, esCorrecta FROM Opcion WHERE idPregunta = @IdPregunta AND esCorrecta = 1";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPregunta", idPregunta);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            opcionCorrecta = new Opcion
                            {
                                IdOpcion = reader.GetInt32(0),
                                Texto = reader.GetString(1),
                                EsCorrecta = reader.GetBoolean(2)
                            };
                        }
                    }
                }
            }

            return opcionCorrecta;
        }
    }
}
