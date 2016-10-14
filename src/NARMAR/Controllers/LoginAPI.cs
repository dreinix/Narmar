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
        [HttpPost]
        public dynamic Post([FromBody]dynamic value)
        {
            var users = _userRepository.AllUsers();
            foreach (var user in users)
            {
                if (user.Active)
                {
                    if (user.Username == (string)value.username && user.Password == (string)value.password)
                    {
                        string token = CreateAccessToken(user.Id.ToString());
                        TokenDatabase[token] = user.Id;
                        Response.Cookies.Append("accessToken", token);
                        Response.Cookies.Append("currentUserId", user.Id.ToString());
                        return new { status = "success", token, uid = user.Id };
                    }
                }
            }
            return new { status = "failed" };
        }

        // DELETE api/login/token
        [HttpDelete("{token}")]
        public void Delete(string token)
        {
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("currentUserId");
            TokenDatabase.Remove(token);
        }
    }
}
