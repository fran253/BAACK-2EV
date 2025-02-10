using System;

public class Pregunta
{
    public int IdPregunta { get; set; }
    public string Enunciado { get; set; }
    public Test Test { get; set; }

    public Pregunta(int idPregunta, string enunciado, Test test)
    {
        if (idPregunta <= 0)
            throw new Exception("el id de la pregunta debe ser un número positivo.");

        if (string.IsNullOrWhiteSpace(enunciado))
            throw new Exception("el enunciado de la pregunta no puede estar vacío.");

        if (test == null || test.IdTest <= 0)
            throw new Exception("el test asociado no es válido.");

        IdPregunta = idPregunta;
        Enunciado = enunciado;
        Test = test;
    }

    public Pregunta() { }

    public void MostrarDetalles()
    {
        Console.WriteLine($"pregunta: {Enunciado}, id pregunta: {IdPregunta}, id test: {Test.IdTest}, título test: {Test.Titulo}");
    }
}
