using System;
using System.Linq;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

            command.GenreId = 1;

            command.Model = new UpdateGenreDtoModel
            {
                Name = "Test Name",
                IsActive = false,
            };
            
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            // Properties of update genre should be equal to the model 
            // inside the command object
            genre.Name.Should().Be(command.Model.Name);
            genre.IsActive.Should().Be(command.Model.IsActive);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

            command.GenreId = 5;
            // Since the GenreId is invalid 
            // an exception will be thrown before the Model property used
            // so it is unnecessary to set
            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("No such genre.!");
        }

        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public void WhenDefaultValueIsGiven_Genre_ShouldNotBeUpdated(string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

            command.GenreId = 1;

            // An UpdateGenreDtoModel with default value
            command.Model = new UpdateGenreDtoModel
            {
                Name = name
                // Since there is not default value for a boolean property
                // it was not set
            };
            
            var before = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);

            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();
            
            var after = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
    
            // The genre should remain as it is after update operation
            after.Name.Should().Be(before.Name);
        }
    }
}