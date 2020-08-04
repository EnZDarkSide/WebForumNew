using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebForumNew.Classes;
using WebForumNew.Data;

namespace WebForumNew.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        ApplicationContext dbContext = new ApplicationContext();

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return dbContext.Users.FirstOrDefault(user => user.ID == id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public List<User> GetAll()
        {
            return dbContext.Users.ToList();
        }
    }
}
