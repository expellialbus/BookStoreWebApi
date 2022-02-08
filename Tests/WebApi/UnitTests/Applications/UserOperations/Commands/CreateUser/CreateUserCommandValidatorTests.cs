using FluentAssertions;
using WebApi.Applications.UserOperations.Commands.CreateUser;
using Xunit;

namespace UnitTests.Applications.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            CreateUserCommand command = new CreateUserCommand(null, null);
            CreateUserCommandValidator validator = new CreateUserCommandValidator();

            command.Model = new CreateUserDtoModel
            {
                Name = "Test Name",
                Surname = "Test Surname",
                Email = "test@mail.com",
                Password = "password"
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("Name", "Surname", "Email", "")]
        [InlineData("Name", "Surname", "", "Password")]
        [InlineData("Name", "", "Email", "Password")]
        [InlineData("", "Surname", "Email", "Password")]
        [InlineData("      ", "Surname", "Email", "Password")]
        [InlineData("Name", "         ", "Email", "Password")]
        [InlineData("Name", "Surname", "       ", "Password")]
        [InlineData("Name", "Surname", "Email", "          ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string name, string surname, string email,
            string password)
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            CreateUserCommand command = new CreateUserCommand(null, null);
            CreateUserCommandValidator validator = new CreateUserCommandValidator();

            command.Model = new CreateUserDtoModel
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}