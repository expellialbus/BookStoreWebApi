using FluentAssertions;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Since this scenario tests the validator 
            // the command object's property won't be used
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();

            command.AuthorId = 5; // it does not matter the book with provided id in database

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        {
            // Since this scenario tests the validator 
            // the command object's property won't be used
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();

            command.AuthorId = 0; // it does not matter the book with provided id in database

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}