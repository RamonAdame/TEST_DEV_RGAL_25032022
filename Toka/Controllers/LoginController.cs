using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toka.Controllers
{
    public class LoginController : Controller
    {
        //OBTENER LA VISTA DE LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
