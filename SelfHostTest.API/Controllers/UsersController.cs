using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfHostTest.API.Domain;
using SelfHostTest.API.Domain.Users;

namespace SelfHostTest.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public ActionResult Post(UserInputModel user)
        {
//            UserViewModel createdUser = new UserViewModel();
//            createdUser.id = 1;
//            createdUser.username = user.username;
//            createdUser.about = user.about;
//
//            return Created($"/users/{createdUser.id}", createdUser);
            userService.CreateUser(user);
            return new EmptyResult();
        }
    }
}