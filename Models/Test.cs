using System.Diagnostics;

public class Test {

    public int IdTest {get;set;}
    public string Titulo {get;set;}
    public DateTime FechaCreacion {get;set;}

     // Clave foránea Temario
    public int IdTemario { get; set; }  
    public Temario Temario { get; set; } 

    public Test(int idTest, string titulo, DateTime fechaCreacion, int idTemario ) {
    IdTest = idTest;
    Titulo = titulo;
    FechaCreacion = fechaCreacion;
    IdTemario = idTemario;

    }
    public Test(){}

}
