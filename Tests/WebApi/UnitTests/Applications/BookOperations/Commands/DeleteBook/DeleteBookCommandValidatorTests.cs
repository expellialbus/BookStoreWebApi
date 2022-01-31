using FluentAssertions;
using FluentValidation;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using Xunit;

namespace UnitTests.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            DeleteBookCommand command = new DeleteBookCommand(null);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            command.BookId = 5; // it does not matter the book with provided id in database

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            DeleteBookCommand command = new DeleteBookCommand(null);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            command.BookId = 0; // it does not matter the book with provided id in database
            
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}