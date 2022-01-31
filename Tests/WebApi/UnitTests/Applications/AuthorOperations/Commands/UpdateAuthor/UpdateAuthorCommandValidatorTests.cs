using FluentAssertions;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Since this scenario tests the validator 
            // the command object's property won't be used
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

            command.AuthorId = 5; // it does not matter the author with provided id in database

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        {
            // Since this scenario tests the validator 
            // the command object's property won't be used
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

            command.AuthorId = 0; // it does not matter the author with provided id in database

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}