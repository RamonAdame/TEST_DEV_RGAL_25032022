using System;
using System.Collections.Generic;
using System.Text;
using Toka.Domain.DTO;

namespace Toka.Services.Contracts
{
    public interface IAuthService
    {
        public bool ValidateLogin(Tb_LoginDTO login);
        string GenerateToken(DateTime fechaActual, string username, TimeSpan tiempoValidez);
    }

}
