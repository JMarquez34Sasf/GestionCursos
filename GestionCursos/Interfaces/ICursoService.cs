using GestionCursos.Models.Dtos;

namespace GestionCursos.Interfaces
{
    public interface ICursoService
    {

        Task<CursoResponse> CreateService(CursoCreate dto);
        Task<List<CursoResponse>> GetService();
        Task<CursoResponse> GetByIdService(int id);
    }
}
