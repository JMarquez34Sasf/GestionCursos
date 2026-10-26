namespace GestionCursos.Models.Dtos
{
    public class UsuarioResponse
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Rol { get; set; } = null!;
    }
}
