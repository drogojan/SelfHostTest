using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SelfHostTest.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post(UserInputModel user)
        {
            UserViewModel createdUser = new UserViewModel();
            createdUser.id = 1;
            createdUser.username = user.username;
            createdUser.about = user.about;

            return Created($"/users/{createdUser.id}", createdUser);
        }
    }

    public class UserViewModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string about { get; set; }
    }

    public class UserInputModel
    {
        public string username { get; set; }
        public string about { get; set; }
    }
}