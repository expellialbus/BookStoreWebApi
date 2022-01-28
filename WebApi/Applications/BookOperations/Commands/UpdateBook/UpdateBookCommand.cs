using System;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using WebApi.DbOperations;

namespace WebApi.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookDtoModel Model { get; set; }

        private readonly IBookStoreDbContext _context;

        public UpdateBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            // Checks database to ensure there is a book with provided id
            var book = _context.Books.SingleOrDefault(book => book.Id == BookId);

            // If there is no book with provided id
            // an exception will be thrown
            if (book is null)
                throw new InvalidOperationException("No such book.!");

            // Properties of the book with provided id will not be updated
            // if the sent inputs are equals to default values
            book.Title = Model.Title.Trim() == "" ? book.Title : Model.Title;
            book.GenreId = Model.GenreId == 0 ? book.GenreId : Model.GenreId;
            book.AuthorId = Model.AuthorId == 0 ? book.AuthorId : Model.AuthorId;
            book.PageCount = Model.PageCount == 0 ? book.PageCount : Model.PageCount;
            book.PublishDate = Model.PublishDate > DateTime.Now ? book.PublishDate : Model.PublishDate;
            
            _context.SaveChanges();
        }
    }

    public class UpdateBookDtoModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}