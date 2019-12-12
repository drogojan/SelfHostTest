using System;
using FluentAssertions;
using Moq;
using SelfHostTest.API.Domain.Users;
using Xunit;

namespace SelfHostTest.UnitTests.Domain.Users
{
    public class UserServiceShould
    {
        private static readonly string USERNAME = "Alice";
        private static readonly string PASSWORD = "Alice123";
        private static readonly string ABOUT = "About Alice";
        private static readonly UserInputModel REGISTRATION_DATA =
            new UserInputModel { Username = USERNAME, Password = PASSWORD, About = ABOUT };

        [Fact]
        public void Create_a_user()
        {
            // TODO - investigate Moq equality comparer
            User user = new User { Username = USERNAME, Password = PASSWORD, About = ABOUT };
            User createdUser = new User { Id = 1, Username = USERNAME, Password = PASSWORD, About = ABOUT };
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.Add(
                It.Is<User>(u =>
                        u.Username == user.Username
                        && u.Password == user.Password
                        && u.About == user.About))).Returns(createdUser);

            UserService sut = new UserService(userRepositoryMock.Object);
            UserApiModel userApiModel = sut.CreateUser(REGISTRATION_DATA);

            userRepositoryMock.Verify(
                m => m.Add(
                        It.Is<User>(u =>
                                                    u.Username == user.Username
                                                    && u.Password == user.Password
                                                    && u.About == user.About)));
            userApiModel.Id.Should().Be(createdUser.Id);
            userApiModel.Username.Should().Be(createdUser.Username);
            userApiModel.About.Should().Be(createdUser.About);
        }

        [Fact]
        public void Throws_UsernameAlreadyInUseException_when_creating_a_user_with_an_existing_username()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.IsUsernameTaken(USERNAME)).Returns(true);

            UserService sut = new UserService(userRepositoryMock.Object);
            Action action = () => sut.CreateUser(REGISTRATION_DATA);

            action.Should().Throw<UsernameAlreadyInUseException>();
        }
    }
}