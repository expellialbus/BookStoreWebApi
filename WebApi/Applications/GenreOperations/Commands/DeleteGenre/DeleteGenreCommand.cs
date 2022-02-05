using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            // Gets book specified by GenreId property
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == GenreId);

            // If there is no such genre throws an exception
            if (genre is null)
                throw new InvalidOperationException("No such genre.!");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}