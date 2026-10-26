using GestionCursos.Models;

namespace GestionCursos.Interfaces
{
    public interface IUsuairo
    {
        Task<Usuario> Create(Usuario usuario);
        Task<Usuario?> Get(int id);
        Task<List<Usuario>> GetAll(string? rol);
        Task<bool> ExisteCorreo(string correo);
    }
}
