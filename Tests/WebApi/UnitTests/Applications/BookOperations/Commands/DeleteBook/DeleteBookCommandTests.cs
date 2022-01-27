using System;
using System.Linq;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeDeleted()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);

            command.BookId = 1;

            FluentActions
                .Invoking(() => command.Handle())
                .Invoke(); 

            // Checks whether the book specified by id deleted or not
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            
            // Returned result should be null if the book is deleted
            book.Should().BeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);

            command.BookId = 5; // there is no such book with id 5 in the provided database

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("No such book.!");
        }
    }
}