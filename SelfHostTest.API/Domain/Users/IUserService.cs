using SelfHostTest.API.Controllers;
using SelfHostTest.API.Models;

namespace SelfHostTest.API.Domain.Users
{
    public interface IUserService
    {
        UserApiModel CreateUser(UserInputModel userInputModel);
    }
}