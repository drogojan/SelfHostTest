using System;
using System.Net.Http;
using Moq;
using SelfHostTest.API.Controllers;
using SelfHostTest.API.Domain;
using SelfHostTest.API.Domain.Users;
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

        [Fact]
        public void Return_the_newly_created_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            var userInputModel = new UserInputModel { username = "Alice", about = "About Alice" };
            UserViewModel USER = new UserViewModel() {id = 1, username = "Alice", about = "About Alice"};
            userServiceMock.Setup(m => m.CreateUser(userInputModel)).Returns(USER);

            UsersController usersController = new UsersController(userServiceMock.Object);
            HttpResponseMessage createUserResponse = usersController.Post(userInputModel);

            userServiceMock.Verify(m => m.CreateUser(userInputModel));
        }
    }
}
