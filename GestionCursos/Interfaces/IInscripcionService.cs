using GestionCursos.Models.Dtos;

namespace GestionCursos.Interfaces
{
    public interface IInscripcionService
    {
        Task<InscripcionResponse> CreateService(InscripcionCreate dto);
        Task<InscripcionResponse> Get(int id);
        Task<List<InscripcionResponse>> GetByEstudianteService(int idEstudiante);
        Task<List<InscripcionResponse>> GetByCursoService(int idCurso);
        Task<InscripcionResponse> CancelarService(int id);
    }
}
