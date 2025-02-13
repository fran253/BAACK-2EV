using System;

public class Resultado
{
    public int IdResultado { get; set; } 
    public int Puntuacion { get; set; } 
    public DateTime Fecha { get; set; } 

    public int IdUsuario { get; set; } 
    public Usuario? Usuario { get; set; }

    public int IdPregunta { get; set; } 
    public Pregunta? Pregunta { get; set; } 

    public Resultado(int idResultado, int puntuacion, DateTime fecha, Usuario usuario, Pregunta pregunta)
    {
        if (idResultado <= 0)
            throw new Exception("el id del resultado debe ser un número positivo.");

        if (puntuacion < 0)
            throw new Exception("la puntuación no puede ser negativa.");

        if (usuario == null || usuario.IdUsuario <= 0)
            throw new Exception("el usuario asociado no es válido.");

        if (pregunta == null || pregunta.IdPregunta <= 0)
            throw new Exception("la pregunta asociada no es válida.");

        IdResultado = idResultado;
        Puntuacion = puntuacion;
        Fecha = fecha != default ? fecha : throw new Exception("la fecha no puede ser la predeterminada.");
        Usuario = usuario;
        IdUsuario = usuario.IdUsuario;
        Pregunta = pregunta;
        IdPregunta = pregunta.IdPregunta;
    }

    public Resultado() { }

}
