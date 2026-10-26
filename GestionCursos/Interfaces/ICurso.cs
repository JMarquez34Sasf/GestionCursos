using GestionCursos.Models;

namespace GestionCursos.Interfaces
{
    public interface ICurso
    {
        Task<Curso> Create(Curso curso);
        Task<List<Curso>> Get();
        Task<Curso?> GetById(int id);
        Task<Usuario?> GetUsuario(int id);
    }
}
