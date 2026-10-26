using GestionCursos.Interfaces;
using GestionCursos.Models;
using GestionCursos.Models.Dtos;

namespace GestionCursos.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICurso repo;
        public CursoService(ICurso repo)
        {
            this.repo = repo;
        }

        public async Task<CursoResponse> CreateService(CursoCreate dto)
        {
            var instructor = await repo.GetUsuario(dto.IdInstructor);
            if (instructor == null)
                throw new ArgumentException("El instructor no existe");
            if (instructor.Rol != "Instructor")
                throw new ArgumentException("Solo un instructor puede crear cursos");

            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new ArgumentException("El título es obligatorio");
            if (dto.DuracionHoras <= 0)
                throw new ArgumentException("La duración debe ser mayor a 0");
            if (dto.FechaFin <= dto.FechaInicio)
                throw new ArgumentException("La fecha fin debe ser mayor a la fecha inicio");

            var curso = new Curso
            {
                IdInstructor = dto.IdInstructor,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                DuracionHoras = dto.DuracionHoras,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin
            };
            var cursoCreado = await repo.Create(curso);

            return new CursoResponse
            {
                IdCurso = cursoCreado.IdCurso,
                IdInstructor = cursoCreado.IdInstructor,
                Instructor = instructor.Nombre,      
                Titulo = cursoCreado.Titulo,
                Descripcion = cursoCreado.Descripcion,
                DuracionHoras = cursoCreado.DuracionHoras,
                FechaInicio = cursoCreado.FechaInicio,
                FechaFin = cursoCreado.FechaFin,
                Inscritos = 0
            };
        }

        public async Task<CursoResponse> GetByIdService(int id)
        {
            var curso = await repo.GetById(id);
            if (curso == null) throw new ArgumentException("Curso no existe");

            return new CursoResponse
            {
                IdCurso = curso.IdCurso,
                IdInstructor = curso.IdInstructor,
                Instructor = curso.IdInstructorNavigation.Nombre,
                Titulo = curso.Titulo,
                Descripcion = curso.Descripcion,
                DuracionHoras = curso.DuracionHoras,
                FechaInicio = curso.FechaInicio,
                FechaFin = curso.FechaFin,
                Inscritos = curso.Inscripcions.Count(i => i.Estado == "Activa")
            };
        }

        public async Task<List<CursoResponse>> GetService()
        {
            var cursos = await repo.Get();
            return cursos.Select(c => new CursoResponse
            {
                IdCurso = c.IdCurso,
                IdInstructor = c.IdInstructor,
                Instructor = c.IdInstructorNavigation.Nombre,
                Titulo = c.Titulo,
                Descripcion = c.Descripcion,
                DuracionHoras = c.DuracionHoras,
                FechaInicio = c.FechaInicio,
                FechaFin = c.FechaFin,
                Inscritos = c.Inscripcions.Count(i => i.Estado == "Activa")
            }).ToList();
        }
    }
}
