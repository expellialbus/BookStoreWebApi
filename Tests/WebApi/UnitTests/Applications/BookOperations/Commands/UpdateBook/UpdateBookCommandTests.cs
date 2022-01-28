using System;
using System.Linq;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.BookId = 1;
            command.Model = new UpdateBookDtoModel
            {
                Title = "Test Title",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 1,
                PublishDate = DateTime.Now.AddYears(-5)
            };

            // Updates the book
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            // Gets the just updated book
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);

            book.Title.Should().Be(command.Model.Title);
            book.GenreId.Should().Be(command.Model.GenreId);
            book.AuthorId.Should().Be(command.Model.AuthorId);
            book.PageCount.Should().Be(command.Model.PageCount);
            book.PublishDate.Should().Be(command.Model.PublishDate);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.BookId = 6;
            // Since the BookId is invalid 
            // an exception will be thrown before the Model property used
            // so it is unnecessary to set

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("No such book.!");
        }

        [Fact]
        public void WhenDefaultInputsAreGiven_Book_ShouldNotBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.BookId = 1;

            // An UpdateBookDtoModel with default values
            command.Model = new UpdateBookDtoModel
            {
                Title = "    ",
                GenreId = 0,
                AuthorId = 0,
                PageCount = 0,
                PublishDate = DateTime.Now.AddYears(5)
            };

            // Gets the book specified by BookId before operation
            var before = _context.Books.SingleOrDefault(book => book.Id == command.BookId);

            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            // Gets the book specified by BookId after operation
            var after = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            
            // The book has to remain as it is (without updated)
            before.Title.Should().Be(after.Title);
            before.GenreId.Should().Be(after.GenreId);
            before.AuthorId.Should().Be(after.AuthorId);
            before.PageCount.Should().Be(after.PageCount);
            before.PublishDate.Should().Be(after.PublishDate);
        }
    }
}