using Microsoft.Data.SqlClient;
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdOpcion, Texto, EsCorrecta, IdPregunta FROM Opcion";
                using (var command = new SqlCommand(query, connection))
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdOpcion, Texto, EsCorrecta, IdPregunta FROM Opcion WHERE IdOpcion = @IdOpcion";
                using (var command = new SqlCommand(query, connection))
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
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Opcion (Texto, EsCorrecta, IdPregunta) VALUES (@Texto, @EsCorrecta, @IdPregunta)";
                using (var command = new SqlCommand(query, connection))
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
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Opcion SET Texto = @Texto, EsCorrecta = @EsCorrecta, IdPregunta = @IdPregunta WHERE IdOpcion = @IdOpcion";
                using (var command = new SqlCommand(query, connection))
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
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Opcion WHERE IdOpcion = @IdOpcion";
                using (var command = new SqlCommand(query, connection))
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdOpcion, Texto, EsCorrecta FROM Opcion WHERE IdPregunta = @IdPregunta";
                using (var command = new SqlCommand(query, connection))
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
    }
}
