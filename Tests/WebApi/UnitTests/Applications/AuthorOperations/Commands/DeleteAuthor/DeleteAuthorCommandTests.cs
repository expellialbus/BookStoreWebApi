using System;
using System.Linq;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenAllInputsAreValid_Author_ShouldBeDeleted()
        {
            // The author does not have to have a book to be deleted
            // Since all authors in the database has a book in the database
            // a new author has to be created
            Author author = new Author
            {
                Name = "Test Name",
                Surname = "Test Surname",
                BirthDate = DateTime.MinValue
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            
            // The new author id will be 4 due to there are already three authors in the database
            command.AuthorId = 4;
            
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);

            author.Should().BeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

            command.AuthorId = 5;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("No such author.!");
        }

        [Fact]
        public void WhenAuthorHasAnyBookIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

            command.AuthorId = 1;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("The author has book, so can't be deleted.!");
        }
    }
}