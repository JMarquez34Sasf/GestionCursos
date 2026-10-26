using GestionCursos.Data;
using GestionCursos.Interfaces;
using GestionCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionCursos.Repositories
{
    public class UsuarioRepository : IUsuairo
    {
        private readonly GestionCursosContext context;
        public UsuarioRepository(GestionCursosContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> Get(int id)
        {
            return await context.Usuarios.FindAsync(id);
        }

        // rol null = todos
        public async Task<List<Usuario>> GetAll(string? rol)
        {
            var query = context.Usuarios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(rol))
                query = query.Where(u => u.Rol == rol);
            return await query.OrderBy(u => u.Nombre).ToListAsync();
        }

        public async Task<bool> ExisteCorreo(string correo)
        {
            return await context.Usuarios.AnyAsync(u => u.Correo == correo);
        }
    }
}
