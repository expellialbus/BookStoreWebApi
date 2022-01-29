using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Theory]
        [InlineData("Name", "Surname")]
        [InlineData("Name", "Ries")]
        [InlineData("Eric", "Surname")]
        public void WhenNotExistNameAndSurnameAreGiven_Author_ShouldBeCreated(string name, string surname)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);

            command.Model = new CreateAuthorDtoModel
            {
                Name = name,
                Surname = surname,
                BirthDate = DateTime.MinValue
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Name == name && author.Surname == surname);

            // If the author added successfully to the database
            // the author variable should not be null
            author.Should().NotBeNull();

            // and the BirthDate property of gotten author has to match with the created one
            author.BirthDate.Should().Be(command.Model.BirthDate);

            // Since the Name and Surname properties are used to get the author from database
            // They are not been required to be checked
        }

        [Fact]
        public void WhenAlreadyExistNameAndSurnameAreGiven_InvalidOperationException_ShouldBeThrown()
        {
            // To test this scenario an author has to be added to database 
            // before checking its presence
            Author author = new Author
            {
                Name = "Test Name",
                Surname = "Test Surname",
                BirthDate = DateTime.Now
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);

            command.Model = new CreateAuthorDtoModel
            {
                Name = author.Name,
                Surname = author.Surname,
                // Since BirthDate property is not used for checking presence of an author
                // it is unnecessary to be assigned
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("The author is already in database");
        }
    }
}