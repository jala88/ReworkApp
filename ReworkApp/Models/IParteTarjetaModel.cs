﻿using ReworkApp.Entities;

namespace ReworkApp.Models
{
    public interface IParteTarjetaModel
    {
        Respuesta ViewBagTarjetas();

        Respuesta ConsultarPartesTarjetas();

    }
}
