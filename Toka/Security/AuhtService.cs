using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Toka.Domain.DTO;
using Toka.Persistence;
using Toka.Services.Contracts;

namespace Toka.Services
{
    public class AuhtService : IAuthService
    {
        private readonly TokaContext _context;

        public AuhtService(TokaContext context)
        {
            _context = context;
        }
        public bool ValidateLogin(Tb_LoginDTO login)
        {
            //var userDB = _context.Tb_Login.Find(login.Username);
            if (login.Username.Equals("ramon_gerardo@outlook.com") && login.Password.Equals("123456"))
                return true;
            return false;
        }

        public string GenerateToken(DateTime fechaActual, string username, TimeSpan tiempoValidez)
        {
            var fechaExpiracion = fechaActual.Add(tiempoValidez);
            //CONFIGURAMOS CLAIMS
            var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(fechaActual).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
                };

            //AÑADIMOS LAS CREDENCIALES
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("6ECFAA5B-9B01-4D15-A610-CE1C4094F49F")),
                SecurityAlgorithms.HmacSha256Signature
                );//SE DEBE CONFIGURAR PAARA OBTENER LOS VALORS DEL APP SETTINGS

            //CONFIGURACION DEL JWT TOKEN
            var jwt = new JwtSecurityToken(
                issuer: "Toka",
                audience: "Public",
                claims: claims,
                notBefore: fechaActual,
                expires: fechaExpiracion,
                signingCredentials: signingCredentials
                );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
