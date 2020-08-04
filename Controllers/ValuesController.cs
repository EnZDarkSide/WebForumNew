using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebForumNew.Controllers
{
    [ApiController]
    [Route("api/reg")]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [HttpGet("/getlogin")]
        public string GetLogin()
        {
            return User.Identity.Name;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/getrole")]
        public string GetRole()
        {
            return $"Ваша роль: Админ";
        }

    }
}