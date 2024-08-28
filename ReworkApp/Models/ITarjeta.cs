using ReworkApp.Entities;

namespace ReworkApp.Models
{
    public interface ITarjeta
    {
        Respuesta RegistrarTarjeta(Tarjeta ent);
        Respuesta ConsultarTarjetas();
    }
}
