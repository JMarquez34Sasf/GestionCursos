using System;
using System.Collections.Generic;

namespace GestionCursos.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    public int IdInstructor { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int DuracionHoras { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public virtual Usuario IdInstructorNavigation { get; set; } = null!;

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
