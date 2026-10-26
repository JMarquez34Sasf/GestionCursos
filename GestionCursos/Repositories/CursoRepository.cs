using GestionCursos.Data;
using GestionCursos.Interfaces;
using GestionCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionCursos.Repositories
{
    public class CursoRepository : ICurso
    {
        private readonly GestionCursosContext context;
        public CursoRepository(GestionCursosContext context)
        {
            this.context = context;
        }

        public async Task<Curso> Create(Curso curso)
        {
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();
            return curso;
        }

        public async Task<List<Curso>> Get()
        {
            var cursos = await context.Cursos
                .Include(c => c.IdInstructorNavigation)
                .Include(c => c.Inscripcions)
                .OrderBy(c => c.FechaInicio)
                .ToListAsync();
            return cursos;
        }

        public async Task<Curso?> GetById(int id)
        {
            var curso = await context.Cursos
                .Include(c => c.IdInstructorNavigation)
                .Include(c => c.Inscripcions)
                .FirstOrDefaultAsync(c => c.IdCurso == id);
            return curso;
        }

        public async Task<Usuario?> GetUsuario(int id)
        {
            return await context.Usuarios.FindAsync(id);
        }
    }
}
