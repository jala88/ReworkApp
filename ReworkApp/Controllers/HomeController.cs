using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ReworkApp.Entities;
using ReworkApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ReworkApp.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController(IUsuarioModel iUsuarioModel,IComunModel iComunModel) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario ent)
        {
            ent.Contrasenna = iComunModel.Encrypt(ent.Contrasenna!);
            var resp = iUsuarioModel.IniciarSesion(ent);

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<Usuario>((JsonElement)resp.Contenido!);
                HttpContext.Session.SetString("TOKEN", datos!.Token!);
                HttpContext.Session.SetString("NOMBRE", datos!.Nombre!);
                HttpContext.Session.SetInt32("CONSECUTIVO", datos!.id_usuario);
                return RedirectToAction("Inicio", "Home");
            }

            ViewBag.msj = resp.Mensaje;
            return View();
        }

        [HttpGet]
        public IActionResult Inicio()
        {
            return View();
        }

        //Solicitudes //

        public IActionResult Solicitudes()
        {
            return View();
        }

        public IActionResult AgregarSolicitud()
        {
            return View();
        }

        //Tarjetas //
        public IActionResult Tarjetas()
        {
            return View();
        }
        public IActionResult RegistrarTarjeta()
        {
            return View();
        }

        //Partes de la Tarjetas //

        public IActionResult PartesTarjetas()
        {
            return View();
        }

        

        public IActionResult Usuarios()
        {
            return View();
        }

        public IActionResult Errores()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
