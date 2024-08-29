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

        public Respuesta ViewBagTarjetasSolicitud()
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Solicitud/ViewBagTarjetasSolicitud";
                var resp = httpClient.GetAsync(url).Result;


                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();

            }


        }

        public Respuesta ViewBagPartesTarjetasId(int id_tarjeta)
        {
            using (httpClient)
            {

                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + $"Solicitud/ViewBagPartesTarjetasId?id_tarjeta={id_tarjeta}";

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


        public Respuesta RegistrarParteTarjeta(ParteTarjeta ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "ParteTarjeta/RegistrarParteTarjeta";
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
