using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NARMAR.Controllers
{
    public class ComponentController : Controller
    {
        // GET: /Component/App
        public IActionResult App()
        {
            return PartialView("App");
        }
        // GET: /Component/Toolbar
        public IActionResult Toolbar()
        {
            return PartialView("Toolbar");
        }
        // GET: /Component/Slideshow
        public IActionResult Slideshow()
        {
            return PartialView("Slideshow");
        }
        // GET: /Component/LoginDialog
        public IActionResult LoginDialog()
        {
            return PartialView("LoginDialog");
        }
    }
}
