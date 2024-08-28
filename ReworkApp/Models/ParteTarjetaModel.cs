using Microsoft.Extensions.Configuration;
using ReworkApp.Entities;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ReworkApp.Models
{
    public class ParteTarjetaModel(HttpClient httpClient, IConfiguration iConfiguration, IHttpContextAccessor iContextAccesor) : IParteTarjetaModel
    {
        public Respuesta ViewBagTarjetas()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "ParteTarjeta/ViewBagTarjetas";
                var resp = httpClient.GetAsync(url).Result;


                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();

            }


        }

        public Respuesta ConsultarPartesTarjetas()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "ParteTarjeta/ConsultarPartesTarjetas";
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
