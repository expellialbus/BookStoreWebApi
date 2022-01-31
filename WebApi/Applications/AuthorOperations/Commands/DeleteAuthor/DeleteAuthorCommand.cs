using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }

        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            // First checks for author's presence
            var author = _context.Authors.SingleOrDefault(author => author.Id == AuthorId);

            // An exception will be thrown if it does not exist
            if (author is null)
                throw new InvalidOperationException("No such author.!");

            // Since an author who has books can't be deleted
            // books of author have to be checked
            var book = _context.Books.FirstOrDefault(book => book.AuthorId == AuthorId);
            
            // An exception will be thrown if author has books
            if (book is not null)
                throw new InvalidOperationException("The author has book, so can't be deleted.!");
            
            // The author will be deleted if has not any book
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}