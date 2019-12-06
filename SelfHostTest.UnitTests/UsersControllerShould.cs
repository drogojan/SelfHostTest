using System;
using Moq;
using SelfHostTest.API.Controllers;
using SelfHostTest.API.Domain;
using Xunit;

namespace SelfHostTest.UnitTests
{
    public class UsersControllerShould
    {
        [Fact]
        public void Create_a_new_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();

            UsersController usersController = new UsersController(userServiceMock.Object);

            var userInputModel = new UserInputModel {username = "Alice", about = "About Alice"};
            usersController.Post(userInputModel);

            userServiceMock.Verify(m => m.CreateUser(userInputModel));
        }
    }
}
