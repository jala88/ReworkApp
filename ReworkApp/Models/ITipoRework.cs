using ReworkApp.Entities;

namespace ReworkApp.Models
{
    public interface ITipoRework
    {
        Respuesta RegistrarTipoRework(TipoRework ent);
        Respuesta ConsultarTipoReworks();

        Respuesta ViewBagTipoReworks();
    }
}
