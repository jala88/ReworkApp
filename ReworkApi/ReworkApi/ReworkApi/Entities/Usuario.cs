namespace ReworkApi.Entities
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Contrasenna { get; set; }
        public string? Token { get; set; }
        public string? Estado { get; set; }

        public string? Descripcion { get; set; }

        public int id_perfil { get; set; }
    }
}
