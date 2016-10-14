using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NARMAR.Controllers
{
    public class Vuelos : Controller
    {
        // GET: /Vuelos/
        public IActionResult Comprar()
        {
            return View();
        }
        // GET: /Vuelos/Recibo
        public IActionResult Recibo()
        {
            return View();
        }
    }
}
