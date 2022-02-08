using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.UserOperations.Commands.CreateUser;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace UnitTests.Applications.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenNotRegisteredUserIsGiven_User_ShouldBeCreated()
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);

            command.Model = new CreateUserDtoModel
            {
                Email = "test@mail.com",
                // Since existence of a user is controlled by email
                // other properties are unnecessary
            };
            
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            var user = _context.Users.SingleOrDefault(user => user.Email == command.Model.Email);

            user.Should().NotBeNull();
        }

        [Fact]
        public void WhenAlreadyRegisteredUserIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // To test this scenario 
            // a user should be created first

            User user = new User
            {
                Email = "new@mail.com",
                // Since existence of a user is controlled by email
                // other properties are unnecessary
            };

            _context.Users.Add(user);
            _context.SaveChanges();
                
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);

            command.Model = new CreateUserDtoModel
            {
                Email = user.Email
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("There is already a user with that email.!");
        }
    }
}