namespace GestionCursos.Models.Dtos
{
    public class UsuarioCreate
    {
        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Rol { get; set; } = null!;

    }
}
