using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reto2_api.Repositories
{
    public class UsuariosRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuariosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idUsuario, nombre, apellidos, gmail, telefono, contraseña, idRol FROM Usuario";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {

                            var usuario = new Usuario
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Gmail = reader.GetString(3),
                                Telefono = reader.GetString(4),
                                Contraseña = reader.GetString(5),
                                IdRol = reader.GetInt32(6)
                            };

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }


        public async Task<Usuario> GetByIdAsync(int id)
        {
            Usuario usuario = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idUsuario, nombre, apellidos, gmail, telefono, contraseña, idRol FROM Usuario WHERE idUsuario = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Gmail = reader.GetString(3),
                                Telefono = reader.GetString(4),
                                Contraseña = reader.GetString(5),
                                IdRol = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public async Task AddAsync(Usuario usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Usuario (nombre, apellidos, gmail, telefono, contraseña, idRol) VALUES (@Nombre, @Apellido, @Gmail, @Telefono, @Contraseña, @IdRol)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@Gmail", usuario.Gmail);
                    command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Usuario SET nombre = @Nombre, apellidos = @Apellido, gmail = @Gmail, telefono = @Telefono, Contraseña = @Contraseña, idRol = @IdRol WHERE idUsuario = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@Gmail", usuario.Gmail);
                    command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Usuario WHERE idUsuario = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
