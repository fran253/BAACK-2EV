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

                string query = "SELECT IdRol, Nombre FROM Rol";
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

                string query = "SELECT IdRol, Nombre FROM Rol WHERE IdRol = @Id";
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

                string query = "INSERT INTO Rol (Nombre) VALUES (@Nombre)";
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

                string query = "UPDATE Rol SET Nombre = @Nombre WHERE IdRol = @IdRol";
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

                string query = "DELETE FROM Rol WHERE IdRol = @IdRol";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRol", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    INSERT INTO Rol (Nombre)
                    VALUES 
                    (@Nombre1),
                    (@Nombre2)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre1", "Administrador");
                    command.Parameters.AddWithValue("@Nombre2", "Usuario");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
