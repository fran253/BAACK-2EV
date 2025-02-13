using System;
using Microsoft.AspNetCore.Identity;

public class Usuario 
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Gmail { get; set; }
    public string Telefono { get; set; }
    public string Contraseña { get; set; }
    
    public int IdRol {get; set;}
    public Rol Rol { get; set; }

    public Usuario(int idUsuario, string nombre, string apellido, string gmail, string telefono,string contraseña, Rol rol)
    {
        if (idUsuario <= 0)
            throw new Exception("el id de usuario debe ser un número positivo.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new Exception("el nombre del usuario no puede estar vacío.");

        if (string.IsNullOrWhiteSpace(apellido))
            throw new Exception("el apellido del usuario no puede estar vacío.");

        if (string.IsNullOrWhiteSpace(gmail) || !gmail.Contains("@"))
            throw new Exception("el gmail proporcionado no es válido.");

        if (string.IsNullOrWhiteSpace(telefono) || telefono.Length < 9)
            throw new Exception("el número de teléfono no es válido.");

        if (rol != null && rol.IdRol <= 0)
            throw new Exception("el id de rol debe ser un número positivo.");

        IdUsuario = idUsuario;
        Nombre = nombre;
        Apellido = apellido;
        Gmail = gmail;
        Telefono = telefono;
        Contraseña = contraseña;
        Rol = rol;
        IdRol = Rol.IdRol;
    }

    public Usuario() { }
}
