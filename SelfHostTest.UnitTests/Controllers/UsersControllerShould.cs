using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfHostTest.API.ApiErrors;
using SelfHostTest.API.Controllers;
using SelfHostTest.API.Domain.Users;
using SelfHostTest.API.Models;
using Xunit;

namespace SelfHostTest.UnitTests.Controllers
{
    public class UsersControllerShould
    {
        private static readonly UserInputModel REGISTRATION_DATA = 
            new UserInputModel {Username = "Alice", Password = "Alice123", About = "About Alice"};
        private static readonly UserApiModel USER = 
            new UserApiModel() {Id = 1, Username = "Alice", About = "About Alice"};

        [Fact]
        public void Create_a_new_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();

            UsersController sut = new UsersController(userServiceMock.Object);

            sut.Post(REGISTRATION_DATA);

            userServiceMock.Verify(m => m.CreateUser(REGISTRATION_DATA));
        }

        [Fact]
        public void Return_the_newly_created_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.CreateUser(REGISTRATION_DATA)).Returns(USER);

            UsersController sut = new UsersController(userServiceMock.Object);

            UserApiModel createdUser =
                ((CreatedResult) sut.Post(REGISTRATION_DATA).Result).Value.As<UserApiModel>();

            createdUser.Username.Should().Be(USER.Username);
            createdUser.About.Should().Be(USER.About);
            createdUser.Id.Should().Be(USER.Id);
        }

        [Fact]
        public void Throws_UsernameAlreadyInUseException_when_creating_a_user_with_an_existing_username()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.CreateUser(REGISTRATION_DATA)).Throws<UsernameAlreadyInUseException>();

            UsersController sut = new UsersController(userServiceMock.Object);

            BadRequestObjectResult badRequestResult = sut.Post(REGISTRATION_DATA).Result as BadRequestObjectResult;
            ApiError apiError = new ApiError { Message = "Username already in use" };
            badRequestResult.Value.As<ApiError>().Message.Should().Be("Username already in use");
        }
    }
}
