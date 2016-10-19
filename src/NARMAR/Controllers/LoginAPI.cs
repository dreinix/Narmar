using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MongoDB.Bson;
using NARMAR.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NARMAR.Controllers
{
    [Route("api/login")]
    public class LoginAPI : Controller
    {
        private const string Secret = "NARMAR's Secret";
        private static Dictionary<string, ObjectId> TokenDatabase = new Dictionary<string, ObjectId>();
        private static TimeSpan TokenLife = new TimeSpan(1,0,0,0);

        private readonly IUserRepository _userRepository;
        public LoginAPI(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public static string CreateAccessToken(string userId)
        {
            string accessToken = "";
            accessToken += DateTime.Now.Ticks.ToString() + "_" + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Secret)) + "_" + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(userId));
            return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(accessToken));
        }
        public static bool ValidateAccessToken(string token, ObjectId userId)
        {
            if(token == null)
            {
                return false;
            }
            string[] accessToken = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(token)).ToString().Split('_');
            string secret = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(accessToken[1]));
            DateTime createDate = new DateTime(long.Parse(accessToken[0]));
            DateTime expireDate = DateTime.Now.Subtract(TokenLife);
            return secret == Secret && createDate > expireDate && TokenDatabase.ContainsKey(token) && TokenDatabase[token] == userId;
        }

        // POST api/login
        [HttpGet("{token}")]
        public dynamic Get(string token)
        {
            if (TokenDatabase.ContainsKey(token))
                return new { status = "success", uid = TokenDatabase[token] };
            return new { status = "failed" };
        }
        // POST api/login
        [HttpPost]
        public dynamic Post([FromBody]dynamic value)
        {
            var users = _userRepository.AllActive();
            foreach (var user in users)
            {
                if (user.username == (string)value.username && user.password == (string)value.password)
                {
                    string token = CreateAccessToken(user._id.ToString());
                    TokenDatabase[token] = user._id;
                    return new { status = "success", token, uid = user._id };
                }
            }
            return new { status = "failed" };
        }
        [HttpDelete("{token}")]
        public void Delete(string token)
        {
            TokenDatabase.Remove(token);
        }
    }
}
