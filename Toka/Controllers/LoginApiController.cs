using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toka.Domain.Contracts;
using Toka.Domain.DTO;
using Toka.Domain.Models;
using Toka.Services.Contracts;

namespace Toka.Controllers
{
    [Route("LoginApi/")]
    public class LoginApiController : Controller
    {
        private readonly IAuthService _authService;

        public LoginApiController(IAuthService authService)
        {
            _authService = authService;
        }

        //INICIO DE SESION CON AUTENTICACION POR TOKEN
        [HttpPost("Login")]
        public IActionResult Token([FromBody]Tb_LoginDTO login)
        {
            if (_authService.ValidateLogin(login))
            {
                var fechaActual = DateTime.UtcNow;
                var validez = TimeSpan.FromHours(5);
                var fechaExpiracion = fechaActual.Add(validez);

                var token = _authService.GenerateToken(fechaActual, login.Username, validez);

                var response = new TokenDTO
                {
                    Tokens = token,
                    ExpireAt = fechaExpiracion
                };

                return Ok(response);
            }
            return StatusCode(401,"Correo/ Contraseña Invalidos");
        }
    }
}
