using Microsoft.Extensions.Configuration;
using ReworkApp.Entities;
using System.Net.Http.Headers;
using System.Net.Http;

namespace ReworkApp.Models
{
    public class TarjetaModel (HttpClient httpClient, IConfiguration iConfiguration, IHttpContextAccessor iContextAccesor) : ITarjeta
    {
        public Respuesta RegistrarTarjeta(Tarjeta ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Tarjeta/RegistrarTarjeta";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }

        public Respuesta ConsultarTarjetas()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Tarjeta/ConsultarTarjetas";
                string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = httpClient.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }


    }
}
