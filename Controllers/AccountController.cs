using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebForumNew.Classes;
using WebForumNew.Data;

namespace WebForumNew.Controllers
{
    [Route("api/user")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        [HttpPost("/login")]
        public IActionResult Login([FromBody]LoginPasswordRequest request)
        {
            var identity = GetIdentity(request.username, request.password);

            if (identity == null)
                return BadRequest(new { errorText = "Неверное имя пользователя или пароль." });

            string encodedJwt = TokenGenerator.GetJWTToken(identity);

            User user = db.Users.FirstOrDefault(user => user.Username == request.username);

            var result = new
            {
                id = user.ID,
                username = user.Username,
                role = user.Role,
                token = encodedJwt,
            };

            return Json(result);
        }

        [HttpPost("/register")]
        public IActionResult Register([FromBody] LoginPasswordRequest request)
        {
            if (db.Users.FirstOrDefault(user => user.Username == request.username) != null)
                return BadRequest(new { errorText = "Пользователь уже существует" });
            if (db.Users.FirstOrDefault(user => user.Email == request.email) != null)
                return BadRequest(new { errorText = "Пользователь с такой почтой уже существует" });

            var user = new User
            {
                Username = request.username,
                Email = request.email,
            };
            
            db.Users.Add(user);
            db.SaveChanges();

            var currentUserId = db.Users.FirstOrDefault(u => u.Username == request.username).ID;

            var password = new Password
            {
                UserId = currentUserId,
                Pwd = request.password,
            };

            db.Passwords.Add(password);
            db.SaveChanges();

            return Login(request);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = db.Passwords.Include(pwd => pwd.User).FirstOrDefault(obj => obj.User.Username == username && obj.Pwd == password).User;

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователь не найден
            return null;
        }
    }
}