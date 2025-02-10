using System;
using Microsoft.AspNetCore.Identity;

public class Rol 
{
    public int IdRol { get; set; }
    public string Nombre { get; set; }


    public Rol(int idRol)
    {
        if (IdRol <= 0)
            throw new Exception("el id de usuario debe ser un número positivo.");

        IdRol = idRol;
    }
    public Rol(int idUsuario, string nombre)
    {
        if (IdRol <= 0)
            throw new Exception("el id de usuario debe ser un número positivo.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new Exception("el nombre del usuario no puede estar vacío.");

        IdRol = idUsuario;
        Nombre = nombre;

    }

    public Rol() { }
    public void MostrarDetalles()
    {
        Console.WriteLine($"Id: {IdRol} , Rol: {Nombre}");
    }
}
