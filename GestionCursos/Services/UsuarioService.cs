using GestionCursos.Interfaces;
using GestionCursos.Models;
using GestionCursos.Models.Dtos;

namespace GestionCursos.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuairo repo;
        public UsuarioService(IUsuairo repo)
        {
            this.repo = repo;
        }

        public async Task<UsuarioResponse> CreateService(UsuarioCreate dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio");
            if (string.IsNullOrWhiteSpace(dto.Correo))
                throw new ArgumentException("El correo es obligatorio");
            if (dto.Rol != "Instructor" && dto.Rol != "Estudiante")
                throw new ArgumentException("El rol debe ser Instructor o Estudiante");
            if (await repo.ExisteCorreo(dto.Correo))
                throw new ArgumentException("Ya existe un usuario con ese correo");

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Rol = dto.Rol
            };
            var usuarioCreado = await repo.Create(usuario);
            return new UsuarioResponse
            {
                IdUsuario = usuarioCreado.IdUsuario,
                Nombre = usuarioCreado.Nombre,
                Correo = usuarioCreado.Correo,
                Rol = usuarioCreado.Rol
            };
        }

        public async Task<UsuarioResponse> GetByIdService(int id)
        {
            var usuario = await repo.Get(id);
            if (usuario == null) throw new ArgumentException("Usuario no existe");
            return new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };
        }

        public async Task<List<UsuarioResponse>> GetService(string? rol)
        {
            var usuarios = await repo.GetAll(rol);
            return usuarios.Select(u => new UsuarioResponse
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Correo = u.Correo,
                Rol = u.Rol
            }).ToList();
        }
    }
}
