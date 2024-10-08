﻿namespace ReworkApp.Entities
{
    public class Usuario
    {
        public int Id_usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Contrasenna { get; set; }
        public string? Token { get; set; }
        public string? Estado { get; set; }

        public string? Descripcion { get; set; }

        public int Id_perfil { get; set; }

        public bool EsTemporal { get; set; }
        public DateTime VigenciaTemporal { get; set; }

    }
}
