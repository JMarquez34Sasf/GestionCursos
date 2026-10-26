namespace GestionCursos.Models.Dtos
{
    public class CursoCreate
    {
        public int IdInstructor { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int DuracionHoras { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
    }
}
