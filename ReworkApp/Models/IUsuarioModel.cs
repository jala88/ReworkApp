﻿using ReworkApp.Entities;

namespace ReworkApp.Models
{
    public interface IUsuarioModel
    {
        Respuesta IniciarSesion(Usuario ent);

        Respuesta RegistrarUsuario(Usuario ent);

        Respuesta ConsultarUsuarios();

        Respuesta ConsultarUsuario(int Consecutivo);
    }
}
