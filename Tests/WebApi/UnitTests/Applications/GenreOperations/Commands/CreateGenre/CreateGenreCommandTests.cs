using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenGivenNameIsNotExist_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);

            // Creates a new genre with not already exist name
            command.Model = new CreateGenreDtoModel
            {
                Name = "Test Name"
            };
            
            // Adds the genre to the database
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == command.Model.Name);

            // If the genre is added successfully, the genre should not be null
            genre.Should().NotBeNull();
        }

        [Fact]
        public void WhenAlreadyExistNameIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // To test this scenario a genre has to be added to database 
            // before checking its presence
            Genre genre = new Genre
            {
                Name = "Test Name"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();
            
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);

            command.Model = new CreateGenreDtoModel
            {
                Name = genre.Name
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("There is a genre with the same name.!");
        }
    }
}