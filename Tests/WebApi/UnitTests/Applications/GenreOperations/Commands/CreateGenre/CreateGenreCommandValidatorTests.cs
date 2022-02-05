using FluentAssertions;
using FluentValidation;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        [Fact]
        public void WhenValidINameIsGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            command.Model = new CreateGenreDtoModel
            {
                Name = "Test Name"
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("")]
        [InlineData("           ")]
        public void WhenInvalidNameIsGiven_Validator_ShouldReturnError(string name)
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            command.Model = new CreateGenreDtoModel
            {
                Name = name
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}