using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReworkApi.Entities;
using ReworkApi.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController(IConfiguration iConfiguration) : ControllerBase
    {

        [HttpGet]
        [Route("ViewBagPartesTarjetasId")]
        public async Task<IActionResult> ViewBagPartesTarjetasId(int id_tarjeta)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<SelectListItem>("ViewBagPartesTarjetasId", new { id_tarjeta }, commandType: CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No se encontraron tarjetas registradas.";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [HttpGet]
        [Route("ViewBagTipoReworks")]
        public async Task<IActionResult> ViewBagTipoReworks()
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<SelectListItem>("ViewBagTipoReworks", new { }, commandType: CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No se encontraron tarjetas registradas.";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [HttpGet]
        [Route("ViewBagUsuarios")]
        public async Task<IActionResult> ViewBagUsuarios()
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<SelectListItem>("ViewBagUsuarios", new { }, commandType: CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No se encontraron usuarios con perfil reworker registrados.";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [HttpGet]
        [Route("ViewBagTarjetasSolicitud")]
        public async Task<IActionResult> ViewBagTarjetasSolicitud()
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<SelectListItem>("ViewBagTarjetasSolicitud", new { }, commandType: CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No se encontraron tarjetas.";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

    }
}
