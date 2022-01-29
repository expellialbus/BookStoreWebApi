using System;
using FluentAssertions;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Since this scenario tests the validator 
            // the command object's property won't be used
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();

            command.Model = new CreateAuthorDtoModel
            {
                Name = "Test Name",
                Surname = "Test Surname",
                BirthDate = DateTime.Now.AddYears(-2)
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
        [Theory]
        [InlineData("", "Test Surname")]
        [InlineData("Test Name", "")]
        [InlineData("Test Name", "         ")]
        [InlineData("      ", "Test Surname")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string name, string surname)
        {
            // Since this scenario tests the validator 
            // the command object's property won't be used
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();

            command.Model = new CreateAuthorDtoModel
            {
                Name = name,
                Surname = surname,
                BirthDate = DateTime.Now.AddYears(-2)
                // Since BirthDate is a dependency (DateTime)
                // it can't be used with Theory attribute
                // It has to be testes separately (see next test case)
                // So to test other properties 
                // it has been given as an already valid value
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenInvalidBirthDateIsGiven_Validator_ShouldReturnError()
        {
            // Since this scenario tests the validator 
            // the command object's property won't be used
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();

            command.Model = new CreateAuthorDtoModel
            {
                Name = "Test Name",
                Surname = "Test Surname",
                BirthDate = DateTime.Now // invalid BirthDate 
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}