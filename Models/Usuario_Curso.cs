public class UsuarioCurso
{
    public int IdUsuario { get; set; } // Clave foránea que referencia a Usuario
    public int IdCurso { get; set; }   // Clave foránea que referencia a Curso

    public Usuario Usuario { get; set; } // Relación con el modelo Usuario
    public Curso Curso { get; set; }     // Relación con el modelo Curso

    public UsuarioCurso(int idUsuario, int idCurso)
    {
        if (idUsuario <= 0)
            throw new Exception("el id del usuario debe ser un número positivo.");
        if (idCurso <= 0)
            throw new Exception("el id del curso debe ser un número positivo.");

        IdUsuario = idUsuario;
        IdCurso = idCurso;
    }

    public UsuarioCurso() { }
}
