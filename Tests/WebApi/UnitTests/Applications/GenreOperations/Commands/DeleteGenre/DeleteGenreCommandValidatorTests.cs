using FluentAssertions;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            command.GenreId = 5; // it does not matter the genre with provided id in database

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            command.GenreId = 0;

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}