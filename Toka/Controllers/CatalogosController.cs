using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toka.Controllers
{
    public class CatalogosController : Controller
    {
        //OBTENER LA VISTA DEL CRUD
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
