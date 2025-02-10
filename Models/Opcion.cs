using System;

public class Opcion
{
    public int IdOpcion { get; set; } 
    public string Texto { get; set; }
    public bool EsCorrecta { get; set; }
    public int IdPregunta { get; set; } 
    public Pregunta Pregunta { get; set; } 

    public Opcion(int idOpcion, string texto, bool esCorrecta, Pregunta pregunta)
    {
        if (idOpcion <= 0)
            throw new Exception("el id de la opción debe ser un número positivo.");

        if (string.IsNullOrWhiteSpace(texto))
            throw new Exception("el texto de la opción no puede estar vacío.");

        if (pregunta == null || pregunta.IdPregunta <= 0)
            throw new Exception("la pregunta asociada no es válida.");

        IdOpcion = idOpcion;
        Texto = texto;
        EsCorrecta = esCorrecta;
        Pregunta = pregunta;
        IdPregunta = pregunta.IdPregunta;
    }

    public Opcion() { }

    public void MostrarDetalles()
    {
        Console.WriteLine($"opción: {Texto}, correcta: {EsCorrecta}, id pregunta: {IdPregunta}");
    }
}
