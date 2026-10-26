namespace GestionCursos.Models.Dtos
{
    public class InscripcionResponse
    {
        public int IdInscripcion { get; set; }

        public int IdCurso { get; set; }

        public string Curso { get; set; } = null!;

        public int IdEstudiante { get; set; }

        public string Estudiante { get; set; } = null!;

        public DateTime FechaInscripcion { get; set; }

        public string Estado { get; set; } = null!;
    }
}
