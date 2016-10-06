using Microsoft.AspNet.Mvc;
using MongoDB.Bson;
using NARMAR.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NARMAR.Controllers
{
    [Route("api/user")]
    public class UserAPI : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserAPI (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/user
        [HttpGet]
        public List<User> Get()
        {
            return _userRepository.AllUsers().ToList<User>();
        }

        // GET: api/user/57cfcfa8dd7b950f10302a67/image
        [HttpGet("{id:length(24)}/image")]
        public IActionResult GetImage(string id,int size = 58,int left = 4, int top = 12)
        {
            User user = _userRepository.GetById(new ObjectId(id));
            Image img = new Bitmap(size,size);
            Graphics g = Graphics.FromImage(img);
            Brush textBrush = Brushes.White;

            Array colors = Enum.GetValues(typeof(KnownColor));
            //g.Clear(Color.FromKnownColor((KnownColor)colors.GetValue(new Random().Next(0,colors.Length-1))));
            g.Clear(Color.DodgerBlue);
            g.DrawString($"{user.FirstName[0]}{user.LastName[0]}", new Font("Arial", 20),textBrush,left,top);
            g.Save();

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
            return new FileContentResult(stream.ToArray(),"image/gif");
        }

        // GET api/user/57cfcfa8dd7b950f10302a67
        [HttpGet("{id:length(24)}", Name = "GetByIdRoute")]
        public User Get(string id)
        {
            return _userRepository.GetById(new ObjectId(id));
        }

        // POST api/user
        [HttpPost]
        public dynamic Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new { status = "failed", data = user };
            }
            else
            {
                _userRepository.Add(user);
                Context.Response.StatusCode = 201;
                return new { status = "success", data = user };
            }
        }

        // PUT api/user/57cfcfa8dd7b950f10302a67
        [HttpPut("{id}")]
        public dynamic Put(string id, [FromBody]User newData)
        {
            if(LoginAPI.ValidateAccessToken(Request.Headers["X-Access-Token"],new ObjectId(id)))
            {
                User user = _userRepository.GetById(new ObjectId(id));
                _userRepository.Update(new ObjectId(id), newData);
                return new { status = "done", data = newData };
            }
            return new { status = "failed" };
        }

        // DELETE api/user/57cfcfa8dd7b950f10302a67
        [HttpDelete("{id:length(24)}")]
        public List<User> Delete(string id)
        {
            if(_userRepository.Remove(new ObjectId(id)))
            {
                return new List<User>();
            }
            else
            {
                return _userRepository.AllUsers().ToList<User>();
            }
        }
    }
}
