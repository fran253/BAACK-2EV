using System.Diagnostics;

public class Curso {

    public int IdCurso {get;set;}
    public string Nombre {get;set;}
    public string Imagen {get;set;}
    public string Descripcion {get;set;}
    public DateTime FechaCreacion {get;set;}

public Curso(int idCurso, string nombre, string imagen, string descripcion, DateTime fechaCreacion ) {
   IdCurso = idCurso;
   Nombre = nombre;
   Imagen = imagen;
   Descripcion = descripcion;
   FechaCreacion = fechaCreacion;
}
    public Curso(){}

    public void MostrarDetalles() {
        Console.WriteLine($"Bebida: {Nombre}, Descripcion {Descripcion:C} ");
    }
}