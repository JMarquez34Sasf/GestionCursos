using System;
using System.Collections.Generic;

namespace GestionCursos.Models;

public partial class Inscripcion
{
    public int IdInscripcion { get; set; }

    public int IdCurso { get; set; }

    public int IdEstudiante { get; set; }

    public DateTime FechaInscripcion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Curso IdCursoNavigation { get; set; } = null!;

    public virtual Usuario IdEstudianteNavigation { get; set; } = null!;
}
