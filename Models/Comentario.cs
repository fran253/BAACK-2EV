using System.Diagnostics;

public class Comentario {

    public int IdComentario {get;set;}
    public string Contenido {get;set;}
    public DateTime FechaCreacion {get;set;}

     // Clave foránea Usuario
    public int IdUsuario { get; set; }  
    public Usuario Usuario { get; set; } 

     // Clave foránea Archivo
    public int IdArchivo { get; set; }  
    public Archivo Archivo { get; set; } 

public Comentario(int idComentario, string contenido, DateTime fechaCreacion, int idUsuario, int idArchivo ) {
   IdComentario = idComentario;
   Contenido = contenido;
   FechaCreacion = fechaCreacion;
   IdUsuario = idUsuario;
   IdArchivo = idArchivo;

}
    public Comentario(){}

}
