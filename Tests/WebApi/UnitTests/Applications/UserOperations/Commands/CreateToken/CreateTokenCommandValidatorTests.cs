using FluentAssertions;
using WebApi.Applications.UserOperations.Commands.CreateToken;
using Xunit;

namespace UnitTests.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidatorTests
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            CreateTokenCommand command = new CreateTokenCommand(null, null);
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();

            command.Model = new CreateTokenDtoModel
            {
                Email = "test@mail.com",
                Password = "password"
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
        [Theory]
        [InlineData("", "password")]
        [InlineData("test@mail.com", "")]
        [InlineData("          ", "password")]
        [InlineData("test@mail.com", "     ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string email, string password)
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            CreateTokenCommand command = new CreateTokenCommand(null, null);
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();

            command.Model = new CreateTokenDtoModel
            {
                Email = email,
                Password = password
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}