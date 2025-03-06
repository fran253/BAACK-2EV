using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly string _connectionString;

        public RolRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Obtener todos los roles
        public async Task<List<Rol>> GetAllAsync()
        {
            var roles = new List<Rol>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idRol, nombre FROM Rol";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var rol = new Rol
                            {
                                IdRol = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                            };

                            roles.Add(rol);
                        }
                    }
                }
            }
            return roles;
        }

        public async Task<Rol> GetByIdAsync(int id)
        {
            Rol rol = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idRol, nombre FROM Rol WHERE idRol = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            rol = new Rol
                            {
                                IdRol = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                            };
                        }
                    }
                }
            }
            return rol;
        }

        public async Task AddAsync(Rol rol)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Rol (nombre) VALUES (@Nombre)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Rol rol)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Rol SET nombre = @Nombre WHERE idRol = @IdRol";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRol", rol.IdRol);
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Rol WHERE idRol = @IdRol";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRol", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
