using Microsoft.Extensions.Configuration;
using ReworkApp.Entities;
using System.Net.Http;


namespace ReworkApp.Models
{
    public class UsuarioModel(HttpClient httpClient, IConfiguration iConfiguration) 
    {
        public Respuesta IniciarSesion(Usuario ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/IniciarSesion";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }


    }
}
