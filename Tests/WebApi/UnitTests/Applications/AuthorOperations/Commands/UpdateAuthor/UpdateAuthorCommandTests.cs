using System;
using System.Linq;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenNonDefaultInputsAreGiven_Author_ShouldBeUpdated()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            command.AuthorId = 1; // Valid id

            // Updates with this new inputs
            command.Model = new UpdateAuthorDtoModel
            {
                Name = "Test Name",
                Surname = "Test Surname",
                BirthDate = DateTime.MinValue
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            // Gets the author specified by AuthorId from database
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);

            // Checks the properties of author gotten from database
            // to be equal to the properties of command.Model
            author.Name.Should().Be(command.Model.Name);
            author.Surname.Should().Be(command.Model.Surname);
            author.BirthDate.Should().Be(command.Model.BirthDate);
        }

        [Fact]
        public void WhenDefaultInputsAreGiven_Author_ShouldNotBeUpdated()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            command.AuthorId = 1;

            // Model with default inputs
            command.Model = new UpdateAuthorDtoModel
            {
                Name = "",
                Surname = "",
                BirthDate = DateTime.Now.AddHours(1)
            };
            
            // The author specified by id should be gotten from database 
            // before update operation

            var before = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);

            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();
            
            var after = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);

            before.Name.Should().Be(after.Name);
            before.Surname.Should().Be(after.Surname);
            before.BirthDate.Should().Be(after.BirthDate);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            command.AuthorId = 5;
            // Since the AuthorId is invalid 
            // an exception will be thrown before the Model property used
            // so it is unnecessary to set

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("No such author.!");

        }
    }
}