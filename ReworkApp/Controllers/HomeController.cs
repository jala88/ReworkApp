using Microsoft.AspNetCore.Mvc;
using ReworkApp.Models;
using System.Diagnostics;

namespace ReworkApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

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
