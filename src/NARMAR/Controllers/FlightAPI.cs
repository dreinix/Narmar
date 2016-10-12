using Microsoft.AspNet.Mvc;
using MongoDB.Bson;
using NARMAR.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NARMAR.Controllers
{
    [Route("api/flight")]
    public class FlightAPI : Controller
    {
        private readonly IFlightRepository _flightRepository;
        public FlightAPI(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        // GET: api/Flight
        [HttpGet]
        public List<Flight> Get()
        {
            return _flightRepository.AllFlight().ToList<Flight>();
        }



        // GET api/Flight/57cfcfa8dd7b950f10302a67
        [HttpGet("{id:length(24)}", Name = "GetRueById")]
        public Flight Get(string id)
        {
            return _flightRepository.GetById(new ObjectId(id));
        }

        // POST api/Flight
        [HttpPost]
        public dynamic Post([FromBody]Flight flight)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new { status = "failed", data = flight };
            }
            else
            {
                _flightRepository.Add(flight);
                Context.Response.StatusCode = 201;
                return new { status = "success", data = flight };
            }
        }

        // PUT api/Flight/57cfcfa8dd7b950f10302a67
        [HttpPut("{id}")]
        public dynamic Put(string id, [FromBody]Flight newData)
        {
            if (LoginAPI.ValidateAccessToken(Request.Headers["X-Access-Token"], new ObjectId(id)))
            {
                Flight flight = _flightRepository.GetById(new ObjectId(id));
                _flightRepository.Update(new ObjectId(id), newData);
                return new { status = "done", data = newData };
            }
            return new { status = "failed" };
        }

        // DELETE api/Flight/57cfcfa8dd7b950f10302a67
        [HttpDelete("{id:length(24)}")]
        public List<Flight> Delete(string id)
        {
            if (_flightRepository.Remove(new ObjectId(id)))
            {
                return new List<Flight>();
            }
            else
            {
                return _flightRepository.AllFlight().ToList<Flight>();
            }
        }
    }
}
