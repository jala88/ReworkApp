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
    public class AdminController(ITipoRework iTipoReworkModel) : Controller
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
            return RedirectToAction("TipoReworks", "Admin");
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


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

