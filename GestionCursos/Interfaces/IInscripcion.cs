using GestionCursos.Models;

namespace GestionCursos.Interfaces
{
    public interface IInscripcion
    {
        Task<Inscripcion> Create(Inscripcion inscripcion);
        Task<Inscripcion?> Get(int id);
        Task<List<Inscripcion>> GetByEstudiante(int idEstudiante);
        Task<List<Inscripcion>> GetByCurso(int idCurso);
        Task<Usuario?> GetUsuario(int id);
        Task<Curso?> GetCurso(int id);
        Task<bool> TieneInscripcionActiva(int idCurso, int idEstudiante);
        Task<Inscripcion> Update(Inscripcion inscripcion);
    }
}
