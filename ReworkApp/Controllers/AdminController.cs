using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ReworkApp.Entities;
using ReworkApp.Models;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReworkApp.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AdminController(ITipoRework iTipoReworkModel, ITarjeta iTarjetaModel, IParteTarjetaModel iParteTarjetaModel) : Controller
    {
        public IActionResult TipoReworks()
        {
            return View();
        }

        public IActionResult EditarTipoRework()
        {
            return View();
        }

        public IActionResult RegistrarTipoRework()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarTipoRework(TipoRework ent)
        {
            iTipoReworkModel.RegistrarTipoRework(ent);
            return RedirectToAction("ConsultarTipoReworks", "Admin");
        }

        

        [HttpGet]
        public IActionResult ConsultarTipoReworks()
        {
            var resp = iTipoReworkModel.ConsultarTipoReworks();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<TipoRework>>((JsonElement)resp.Contenido!);
                return View(datos);
            }

            return View(new List<TipoRework>());
        }

        [HttpGet]
        public IActionResult ConsultarTarjetas()
        {
            var resp = iTarjetaModel.ConsultarTarjetas();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Tarjeta>>((JsonElement)resp.Contenido!);
                return View(datos);
            }

            return View(new List<Tarjeta>());
        }

        [HttpGet]
        public IActionResult RegistrarTarjeta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarTarjeta(Tarjeta ent)
        {
            iTarjetaModel.RegistrarTarjeta(ent);
            return RedirectToAction("ConsultarTarjetas", "Admin");
        }

        [HttpGet]
        public IActionResult RegistrarParteTarjeta()
        {

            var resp = iParteTarjetaModel.ViewBagTarjetas();
            ViewBag.Tarjetas = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)resp.Contenido!);

            return View();

        }

        [HttpGet]
        public IActionResult ConsultarPartesTarjetas()
        {
            var resp = iParteTarjetaModel.ConsultarPartesTarjetas();

            if (resp.Codigo == 1)
            {
                var datos = JsonSerializer.Deserialize<List<ParteTarjeta>>((JsonElement)resp.Contenido!);
                return View(datos);
            }

            return View(new List<ParteTarjeta>());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

