using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace NARMAR.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return PartialView("Home");
        }
        public IActionResult CreateUser()
        {
            return PartialView("CreateUser");
        }
    }
}
