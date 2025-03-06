using System.Diagnostics;

public class Temario 
{
    public int IdTemario { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }

    // Clave foránea
    public int IdAsignatura { get; set; }  
    public Asignatura Asignatura { get; set; } 

    // Constructor
    public Temario(int idTemario, string titulo, string descripcion, int idAsignatura) 
    {
        IdTemario = idTemario;
        Titulo = titulo;
        Descripcion = descripcion;
        IdAsignatura = idAsignatura;

    }

    public Temario(){}
    public void MostrarDetalles() 
    {
        Console.WriteLine($"Asignatura: {Titulo}, Descripción: {Descripcion}, Asignatura: {(Asignatura != null ? Asignatura.Nombre : "No asignado")}");
    }
}
