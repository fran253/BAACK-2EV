using System.Diagnostics;

public class Asignatura 
{
    public int IdAsignatura { get; set; }
    public string Nombre { get; set; }
    public string Imagen { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Clave foránea
    public int IdCurso { get; set; }
    public Curso Curso { get; set; }

    // Constructor
    public Asignatura(int idAsignatura, string nombre, string imagen, string descripcion, DateTime fechaCreacion, int cursoId) 
    {
        IdAsignatura = idAsignatura;
        Nombre = nombre;
        Imagen = imagen;
        Descripcion = descripcion;
        FechaCreacion = fechaCreacion;
        IdCurso = cursoId;
    }
    public Asignatura(){}

}
