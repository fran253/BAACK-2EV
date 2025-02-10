using System.Diagnostics;
public class Asignatura 
{
    public int IdAsignatura { get; set; }
    public string Nombre { get; set; }
    public string Imagen { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Clave foránea
    public int CursoId { get; set; }
    public Curso Curso { get; set; }

    // Constructor
    public Asignatura(int idAsignatura, string nombre, string imagen, string descripcion, DateTime fechaCreacion, int cursoId) 
    {
        IdAsignatura = idAsignatura;
        Nombre = nombre;
        Imagen = imagen;
        Descripcion = descripcion;
        FechaCreacion = fechaCreacion;
        CursoId = cursoId;
    }
    public Asignatura(){}

    public void MostrarDetalles() 
    {
        Console.WriteLine($"Asignatura: {Nombre}, Descripción: {Descripcion}, Curso: {(Curso != null ? Curso.Nombre : "No asignado")}");
    }
}
