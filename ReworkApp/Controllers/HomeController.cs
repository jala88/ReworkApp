using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReworkApp.Entities;
using ReworkApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ReworkApp.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController(IUsuarioModel iUsuarioModel,IComunModel iComunModel, IRolModel iRolModel) : Controller
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
                HttpContext.Session.SetString("ROL", datos!.id_perfil.ToString());
                HttpContext.Session.SetInt32("CONSECUTIVO", datos!.id_usuario);
                return RedirectToAction("Inicio", "Home");
            }

            ViewBag.msj = resp.Mensaje;
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario ent)
        {
            ent.Contrasenna = iComunModel.Encrypt(ent.Contrasenna!);
            var resp = iUsuarioModel.RegistrarUsuario(ent);

            if (resp.Codigo == 1)
                return RedirectToAction("Index", "Home");

            ViewBag.msj = resp.Mensaje;
            return View();
        }

        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario ent)
        {
            var resp = iUsuarioModel.RecuperarAcceso(ent.Nombre!);

            if (resp.Codigo == 1)
                return RedirectToAction("Index", "Home");

            ViewBag.msj = resp.Mensaje;
            return View();
        }


        [FiltroSesiones]
        [HttpGet]
        public IActionResult ConsultarUsuarios()
        {
            var resp = iUsuarioModel.ConsultarUsuarios();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Usuario>>((JsonElement)resp.Contenido!);
                return View(datos!.Where(x => x.id_usuario != HttpContext.Session.GetInt32("CONSECUTIVO")).ToList());
            }

            return View(new List<Usuario>());
        }

        [FiltroSesiones]
        [HttpGet]
        public IActionResult ActualizarUsuario(int q)
        {
            var roles = iRolModel.ConsultarRoles();
            ViewBag.Roles = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)roles.Contenido!);

            var resp = iUsuarioModel.ConsultarUsuario(q);

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<Usuario>((JsonElement)resp.Contenido!);
                return View(datos);
            }

            return View(new Usuario());
        }

        [FiltroSesiones]
        [HttpPost]
        public IActionResult ActualizarUsuario(Usuario ent)
        {
            var resp = iUsuarioModel.ActualizarUsuario(ent);

            if (resp.Codigo == 1)
                return RedirectToAction("ConsultarUsuarios", "Home");

            ViewBag.msj = resp.Mensaje;
            return View();
        }


        [FiltroSesiones]
        [HttpGet]
        public IActionResult Inicio()
        {
            return View();
        }

        [FiltroSesiones]
        [HttpGet]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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
