using System;
using FluentAssertions;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using Xunit;

namespace UnitTests.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            UpdateBookCommand command = new UpdateBookCommand(null);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            command.BookId = 5; // it does not matter the book with provided is in database
            command.Model = new UpdateBookDtoModel
            {
                // No rule for Title
                GenreId = 0,
                AuthorId = 0,
                PageCount = 0,
                // No rule for PublishDate
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, -1, 0, 0)]
        [InlineData(1, 0, -1, 0)]
        [InlineData(1, 0, 0, -1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int id, int genreId, int authorId, int pageCount)
        {
            // Since this function tests the validator 
            // the command object's property won't be used
            UpdateBookCommand command = new UpdateBookCommand(null);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            command.BookId = id; 
            command.Model = new UpdateBookDtoModel
            {
                // No rule for Title
                GenreId = genreId,
                AuthorId = authorId,
                PageCount = pageCount,
                // No rule for PublishDate
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}