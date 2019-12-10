using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfHostTest.API.ApiErrors;
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
        public ActionResult<UserApiModel> Post(UserInputModel user)
        {
            try
            {
                UserApiModel createdUser = userService.CreateUser(user);

                return Created("", createdUser);
            }
            catch (UsernameAlreadyInUseException)
            {
                return new BadRequestObjectResult(new ApiError { Message = "Username already in use"});
            }

        }
    }
}