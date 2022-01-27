using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }

        private readonly IBookStoreDbContext _context;

        public DeleteBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            // Gets book specified by BookId property
            var book = _context.Books.SingleOrDefault(book => book.Id == BookId);

            // If there is no such book throws an error
            if (book is null)
                throw new InvalidOperationException("No such book.!");

            // Removes book if it is in database
            _context.Books.Remove(book);
            
            // Commits changes to the database
            _context.SaveChanges();
        }
    }
}