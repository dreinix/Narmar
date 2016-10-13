using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NARMAR.Models;
using MongoDB.Driver;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NARMAR.Controllers
{
    public class UserController : Controller
    {

        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        // GET: /User/
        [HttpGet]
        public IActionResult Index()
        {
            return View(_userRepository.AllUsers().ToList<User>());
        }
        // GET: /User/Confirm
        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }
        // GET: /User/Create
        public IActionResult Create()
        {
            return View();
        }
        //GET:: /User/Login/Confirm
        public IActionResult Confirm() {
            return View();
        }

        // GET: /User/Edit
        public IActionResult Edit(string id)
        {
            var user = _userRepository.GetById(new ObjectId(id));
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        // GET: /User/Details/:id
        public IActionResult Details(string id)
        {
            var user = _userRepository.GetById(new ObjectId(id));
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        // GET: /User/Login
        public IActionResult Login()
        {
            return PartialView();
        }
    }
}
