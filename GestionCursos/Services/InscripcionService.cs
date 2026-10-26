using GestionCursos.Interfaces;
using GestionCursos.Models;
using GestionCursos.Models.Dtos;

namespace GestionCursos.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IInscripcion repo;
        public InscripcionService(IInscripcion repo)
        {
            this.repo = repo;
        }

        public async Task<InscripcionResponse> CreateService(InscripcionCreate dto)
        {
            var estudiante = await repo.GetUsuario(dto.IdEstudiante);
            if (estudiante == null)
                throw new ArgumentException("El estudiante no existe");
            if (estudiante.Rol != "Estudiante")
                throw new ArgumentException("Solo un estudiante puede inscribirse");

            var curso = await repo.GetCurso(dto.IdCurso);
            if (curso == null)
                throw new ArgumentException("El curso no existe");
            if (curso.FechaFin <= DateTime.Now)
                throw new ArgumentException("El curso ya terminó");

            if (await repo.TieneInscripcionActiva(dto.IdCurso, dto.IdEstudiante))
                throw new ArgumentException("Ya está inscrito en este curso");

            var inscripcion = new Inscripcion
            {
                IdCurso = dto.IdCurso,
                IdEstudiante = dto.IdEstudiante,
                FechaInscripcion = DateTime.Now,
                Estado = "Activa"
            };
            var registrada = await repo.Create(inscripcion);

            return new InscripcionResponse
            {
                IdInscripcion = registrada.IdInscripcion,
                IdCurso = curso.IdCurso,
                Curso = curso.Titulo,
                IdEstudiante = estudiante.IdUsuario,
                Estudiante = estudiante.Nombre,
                FechaInscripcion = registrada.FechaInscripcion,
                Estado = registrada.Estado
            };
        }

        public async Task<InscripcionResponse> Get(int id)
        {
            var i = await repo.Get(id);
            if (i == null) throw new ArgumentException("Inscripcion no existe");
            return new InscripcionResponse
            {
                IdInscripcion = i.IdInscripcion,
                IdCurso = i.IdCurso,
                Curso = i.IdCursoNavigation.Titulo,
                IdEstudiante = i.IdEstudiante,
                Estudiante = i.IdEstudianteNavigation.Nombre,
                FechaInscripcion = i.FechaInscripcion,
                Estado = i.Estado
            };
        }

        public async Task<List<InscripcionResponse>> GetByEstudianteService(int idEstudiante)
        {
            var inscripciones = await repo.GetByEstudiante(idEstudiante);
            return inscripciones.Select(i => new InscripcionResponse
            {
                IdInscripcion = i.IdInscripcion,
                IdCurso = i.IdCurso,
                Curso = i.IdCursoNavigation.Titulo,
                IdEstudiante = i.IdEstudiante,
                Estudiante = i.IdEstudianteNavigation.Nombre,
                FechaInscripcion = i.FechaInscripcion,
                Estado = i.Estado
            }).ToList();
        }

        public async Task<List<InscripcionResponse>> GetByCursoService(int idCurso)
        {
            var inscripciones = await repo.GetByCurso(idCurso);
            return inscripciones.Select(i => new InscripcionResponse
            {
                IdInscripcion = i.IdInscripcion,
                IdCurso = i.IdCurso,
                Curso = i.IdCursoNavigation.Titulo,
                IdEstudiante = i.IdEstudiante,
                Estudiante = i.IdEstudianteNavigation.Nombre,
                FechaInscripcion = i.FechaInscripcion,
                Estado = i.Estado
            }).ToList();
        }

        public async Task<InscripcionResponse> CancelarService(int id)
        {
            var i = await repo.Get(id);
            if (i == null) throw new ArgumentException("Inscripcion no existe");
            if (i.Estado != "Activa")
                throw new ArgumentException("Solo se puede cancelar una inscripción activa");

            i.Estado = "Cancelada";
            await repo.Update(i);

            return new InscripcionResponse
            {
                IdInscripcion = i.IdInscripcion,
                IdCurso = i.IdCurso,
                Curso = i.IdCursoNavigation.Titulo,
                IdEstudiante = i.IdEstudiante,
                Estudiante = i.IdEstudianteNavigation.Nombre,
                FechaInscripcion = i.FechaInscripcion,
                Estado = i.Estado
            };
        }
    }
}
