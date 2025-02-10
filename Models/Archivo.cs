using System.Diagnostics;

public class Archivo {

    public int IdArchivo {get;set;}
    public string Titulo {get;set;}
    public string Url {get;set;}
    public string Tipo {get;set;}
    public DateTime FechaCreacion {get;set;}

     // Clave foránea Usuario
    public int IdUsuario { get; set; }  
    public Usuario Usuario { get; set; } 

     // Clave foránea Temario
    public int IdTemario { get; set; }  
    public Temario Temario { get; set; } 

public Archivo(int idArchivo, string titulo, string url, string tipo, DateTime fechaCreacion, int idUsuario, int idTemario ) {
   IdArchivo = idArchivo;
   Titulo = titulo;
   Url = url;
   Tipo = tipo;
   FechaCreacion = fechaCreacion;
   IdUsuario = idUsuario;
   IdTemario = idTemario;

}
    public Archivo(){}

    public void MostrarDetalles() {
        Console.WriteLine($"Bebida: {Titulo}, Descripcion {Tipo:C} ");
    }
}