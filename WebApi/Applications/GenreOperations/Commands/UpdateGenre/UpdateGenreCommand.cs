using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreDtoModel Model { get; set; }
            
        private readonly IBookStoreDbContext _context;

        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            // Checks database to ensure there is a genre with provided id
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == GenreId);

            // If there is no genre with provided id
            // an exception will be thrown
            if (genre is null)
                throw new InvalidOperationException("No such genre.!");

            // Properties of the genre with provided id will not be updated
            // if sent name equals to an empty string
            genre.Name = Model.Name.Trim() == "" ? genre.Name : Model.Name;
            
            // Since there is not default value for boolean
            // IsActive property will directly updated 
            genre.IsActive = Model.IsActive;

            // Commits changes to the database
            _context.SaveChanges();
        }
    }

    public class UpdateGenreDtoModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}