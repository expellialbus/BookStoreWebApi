using System;
using FluentAssertions;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using Xunit;

namespace UnitTests.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            command.Model = new CreateBookDtoModel
            {
                Title = "Test Title",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 100,
                // Since validator object created before this  line
                // the value of DateTime.Now statement inside of its constructor
                // will be less than the DateTime.Now value of this line
                // so by subtracting 1 millisecond from the value of this line 
                // will solve this problem
                PublishDate = DateTime.Now.AddMilliseconds(-1)
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("      ", 1, 1, 1)]
        [InlineData("", 1, 1, 1)]
        [InlineData("Test Title", 0, 1, 1)]
        [InlineData("Test Title", 1, 0, 1)]
        [InlineData("Test Title", 1, 1, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string title, int genreId, int authorId,
            int pageCount)
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            command.Model = new CreateBookDtoModel
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId,
                PageCount = pageCount
                // Since PublishDate is a dependency (DateTime)
                // it can't be used with Theory attribute
                // It has to be testes separately (see next test case)
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        // Since PublishDate is a dependency (DateTime)
        // it can't be used with Theory attribute
        // This case tests this circumstance
        [Fact]
        public void WhenInvalidDateIsGiven_Validator_ShouldReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            // All inputs are valid except PublishDate
            command.Model = new CreateBookDtoModel
            {
                Title = "Test Title",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddDays(1)
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}