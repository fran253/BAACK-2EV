using System;

public class ArchivoUsuario
{
    public int IdUsuario { get; set; }
    public int IdArchivo { get; set; }
    public DateTime FechaGuardado { get; set; }

    public ArchivoUsuario(int idUsuario, int idArchivo)
    {
        if (idUsuario <= 0)
            throw new ArgumentException("el id del usuario debe ser un número positivo.", nameof(idUsuario));
        if (idArchivo <= 0)
            throw new ArgumentException("el id del archivo debe ser un número positivo.", nameof(idArchivo));

        IdUsuario = idUsuario;
        IdArchivo = idArchivo;
        FechaGuardado = DateTime.Now;
    }

    public ArchivoUsuario() { }
}
