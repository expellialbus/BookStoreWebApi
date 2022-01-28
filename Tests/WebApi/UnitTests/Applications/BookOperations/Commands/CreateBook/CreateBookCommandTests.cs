using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace UnitTests.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenNotExistTitleIsGiven_Book_ShouldBeCreated()
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            // Creates a new book with not already exist title
            command.Model = new CreateBookDtoModel
            {
                Title = "First Test Title",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddYears(-1)
            };
            
            // Adds the book to the database
            FluentActions
                .Invoking(() => command.Handle())
                .Invoke();

            // Gets the book just added to database
            var book = _context.Books.SingleOrDefault(book => book.Title == command.Model.Title);

            // If the book is added successfully, the book should not be null
            book.Should().NotBeNull();
            
            // and other properties of gotten book have to match with the created one
            book.GenreId.Should().Be(command.Model.GenreId);
            book.AuthorId.Should().Be(command.Model.AuthorId);
            book.PageCount.Should().Be(command.Model.PageCount);
            book.PublishDate.Should().Be(command.Model.PublishDate);
        }

        [Fact]
        public void WhenAlreadyExistTitleIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // To test this scenario a book has to be added to database 
            // before checking its presence
            Book book = new Book
            {
                Title = "Second Test Title",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddYears(-1)
            };

            // Adds book to the database
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            // Since just Titles of books will be checked to test the existence
            // other properties are unnecessary to set
            command.Model = new CreateBookDtoModel
            {
                Title = book.Title
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("There is already a book with this title in the database.!");
        }
    }
}