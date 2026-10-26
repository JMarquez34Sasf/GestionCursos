using GestionCursos.Models.Dtos;

namespace GestionCursos.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> CreateService(UsuarioCreate dto);
        Task<UsuarioResponse> GetByIdService(int id);
        Task<List<UsuarioResponse>> GetService(string? rol);
    }
}
