using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NARMAR.Models;
using MongoDB.Driver;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NARMAR.Controllers
{
    public class FlightController : Controller
    {   /*
        private IFlightRepository _flightRepository;
        public FlightController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;

        }
        */

        // GET: /Flight/Comprar
        public IActionResult Comprar()
        {
            return View();
        }
        // GET: /Flight/Ver
        [HttpGet]
        public IActionResult Ver()
        {
            return View(/*_flightRepository.AllFlight()*/);
        }
        // GET: /Flight/Create
        public IActionResult Create() {
            return View();
        }
    }
}
