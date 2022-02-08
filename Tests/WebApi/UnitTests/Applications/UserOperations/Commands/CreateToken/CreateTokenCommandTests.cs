using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using UnitTests.TestSetup;
using WebApi.Applications.UserOperations.Commands.CreateToken;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace UnitTests.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public CreateTokenCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _configuration = fixture.Configuration;
        }

        [Fact]
        public void WhenValidUserIsGiven_Token_ShouldBeCreated()
        {
            // To test this scenario 
            // a user should be created before test operations
            User user = new User
            {
                Email = "test@mail.com",
                Password = "password"
                // Name and Surname values were not provided 
                // due to they are unnecessary
                // to create a token for a user
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            CreateTokenCommand command = new CreateTokenCommand(_configuration, _context);

            command.Model = new CreateTokenDtoModel
            {
                Email = user.Email,
                Password = user.Password
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().NotBeNull();
        }

        [Fact]
        public void WhenInvalidUserIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            CreateTokenCommand command = new CreateTokenCommand(_configuration, _context);

            command.Model = new CreateTokenDtoModel
            {
                Email = "random@mail.com",
                Password = "random"
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Email or Password is wrong.!");
        }
    }
}