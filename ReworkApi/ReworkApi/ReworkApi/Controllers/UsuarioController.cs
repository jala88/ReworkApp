using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
    public class UsuarioController(IConfiguration iConfiguration, IComunModel iComunModel, IHostEnvironment iHost) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(Usuario ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryFirstOrDefaultAsync<Usuario>("IniciarSesion", new { ent.Correo, ent.Contrasenna }, commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    result.Token = GenerarToken(result.Id_usuario, result.Id_perfil);

                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "La información del usuario no se encuentra registrada";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(Usuario ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarUsuario", new { ent.Nombre, ent.Correo, ent.Contrasenna }, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = true;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "La información del usuario ya se encuentra registrada";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

       
        [HttpGet]
        [Route("ConsultarUsuarios")]
        public async Task<IActionResult> ConsultarUsuarios()
        {
            if (!EsAdministrador())
                return StatusCode(403);

            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryAsync<Usuario>("ConsultarUsuarios", new { }, commandType: CommandType.StoredProcedure);

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
                    resp.Mensaje = "No hay usuarios registrados en este momento";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        
        [HttpGet]
        [Route("ConsultarUsuario")]
        public async Task<IActionResult> ConsultarUsuario(int id_usuario)
        {
            if (!EsAdministrador())
                return StatusCode(403);

            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryFirstOrDefaultAsync<Usuario>("ConsultarUsuario", new { id_usuario }, commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No hay usuarios registrados en este momento";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        
        [HttpPut]
        [Route("ActualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario(Usuario ent)
        {
            

            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("ActualizarUsuario", new { ent.Id_usuario, ent.Nombre, ent.Correo, ent.Id_perfil}, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = true;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "La información del usuario no se pudo actualizar";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [HttpGet]
        [Route("RecuperarAcceso")]
        public async Task<IActionResult> RecuperarAcceso(string Nombre)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.QueryFirstOrDefaultAsync<Usuario>("ConsultarUsuarioNombre", new { Nombre }, commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    var CodigoAleatorio = iComunModel.GenerarCodigo();
                    var Contrasenna = iComunModel.Encrypt(CodigoAleatorio);
                    var EsTemporal = true;
                    var VigenciaTemporal = DateTime.Now.AddMinutes(30);

                    await context.ExecuteAsync("ActualizarContrasenna",
                        new { result.Id_usuario, Contrasenna, EsTemporal, VigenciaTemporal },
                        commandType: CommandType.StoredProcedure);

                    var ruta = Path.Combine(iHost.ContentRootPath, "FormatoCorreo.html");
                    var html = System.IO.File.ReadAllText(ruta);

                    html = html.Replace("@@Nombre", result.Nombre);
                    html = html.Replace("@@Contrasenna", CodigoAleatorio);
                    html = html.Replace("@@Vencimiento", VigenciaTemporal.ToString("dd/MM/yyyy HH:mm"));

                    iComunModel.EnviarCorreo(result.Correo!, "Recuperar Acceso Sistema", html);

                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = result;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No hay usuarios registrados con ese nombre";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [HttpPut]
        [Route("CambiarEstadoUsuario")]
        public async Task<IActionResult> CambiarEstadoUsuario(Usuario ent)
        {


            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("CambiarEstadoUsuario", new { ent.Id_usuario }, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "OK";
                    resp.Contenido = true;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "El estado del usuario no se pudo actualizar";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }



        private string GenerarToken(int id_usuario, int id_perfil)
        {
            string SecretKey = iConfiguration.GetSection("Llaves:SecretKey").Value!;
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, id_usuario.ToString()));
            claims.Add(new Claim("id_perfil", id_perfil.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool EsAdministrador()
        {
            var userrol = User.Claims.Select(Claim => new { Claim.Type, Claim.Value })
                .FirstOrDefault(x => x.Type == "id_perfil")!.Value;

            return (userrol == "1" ? true : false);
        }
    }
}
