namespace GestionCursos.Models.Dtos
{
    public class CursoResponse
    {
        public int IdCurso { get; set; }

        public int IdInstructor { get; set; }

        public string Instructor { get; set; } = null!;

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int DuracionHoras { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int Inscritos { get; set; }
    }
}
