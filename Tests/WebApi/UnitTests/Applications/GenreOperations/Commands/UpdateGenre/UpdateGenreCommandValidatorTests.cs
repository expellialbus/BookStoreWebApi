using FluentAssertions;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            command.GenreId = 1;
            // Since there is no rule for Model property
            // it is unnecessary to set

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            command.GenreId = 0;
            // Since there is no rule for Model property
            // it is unnecessary to set

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}