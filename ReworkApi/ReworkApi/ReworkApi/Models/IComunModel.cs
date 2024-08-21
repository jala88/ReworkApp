using System.Security.Claims;

namespace ReworkApi.Models
{
    public interface IComunModel
    {
        bool EsAdministrador(ClaimsPrincipal User);

        string GenerarCodigo();

        string Encrypt(string texto);

        void EnviarCorreo(string destino, string asunto, string contenido);
    }
}
