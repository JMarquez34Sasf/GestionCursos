using GestionCursos.Data;
using GestionCursos.Interfaces;
using GestionCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionCursos.Repositories
{
    public class InscripcionRepository : IInscripcion
    {
        private readonly GestionCursosContext context;
        public InscripcionRepository(GestionCursosContext context)
        {
            this.context = context;
        }

        public async Task<Inscripcion> Create(Inscripcion inscripcion)
        {
            context.Inscripcions.Add(inscripcion);
            await context.SaveChangesAsync();
            return inscripcion;
        }

        public async Task<Inscripcion?> Get(int id)
        {
            return await context.Inscripcions
                .Include(i => i.IdCursoNavigation)
                .Include(i => i.IdEstudianteNavigation)
                .FirstOrDefaultAsync(i => i.IdInscripcion == id);
        }

        public async Task<List<Inscripcion>> GetByEstudiante(int idEstudiante)
        {
            return await context.Inscripcions
                .Include(i => i.IdCursoNavigation)
                .Include(i => i.IdEstudianteNavigation)
                .Where(i => i.IdEstudiante == idEstudiante)
                .OrderByDescending(i => i.FechaInscripcion)
                .ToListAsync();
        }

        public async Task<List<Inscripcion>> GetByCurso(int idCurso)
        {
            return await context.Inscripcions
                .Include(i => i.IdCursoNavigation)
                .Include(i => i.IdEstudianteNavigation)
                .Where(i => i.IdCurso == idCurso)
                .OrderBy(i => i.FechaInscripcion)
                .ToListAsync();
        }

        public async Task<Usuario?> GetUsuario(int id)
        {
            return await context.Usuarios.FindAsync(id);
        }

        public async Task<Curso?> GetCurso(int id)
        {
            return await context.Cursos.FindAsync(id);
        }

        public async Task<bool> TieneInscripcionActiva(int idCurso, int idEstudiante)
        {
            return await context.Inscripcions.AnyAsync(i =>
                i.IdCurso == idCurso &&
                i.IdEstudiante == idEstudiante &&
                i.Estado == "Activa");
        }

        public async Task<Inscripcion> Update(Inscripcion inscripcion)
        {
            context.Inscripcions.Update(inscripcion);
            await context.SaveChangesAsync();
            return inscripcion;
        }
    }
}
