using System;
using System.Linq;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenExistIdIsGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);

            command.GenreId = 1;

            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            // Checks whether the genre specified by id deleted or not
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            genre.Should().BeNull();
        }

        [Fact]
        public void WhenNonExistIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);

            command.GenreId = 5;  // there is no genre with id 5 in the provided database

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("No such genre.!");
        }
    }
}