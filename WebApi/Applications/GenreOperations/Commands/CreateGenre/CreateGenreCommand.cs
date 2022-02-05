using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreDtoModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            // Checks database to ensure there is no any genre with provided name
            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == Model.Name);

            // If there is already a genre with the name 
            // an exception will be thrown
            if (genre is not null)
                throw new InvalidOperationException("There is a genre with the same name.!");

            // If there is not, new genre will be written to the database
            genre = _mapper.Map<Genre>(Model);

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreDtoModel
    {
        public string Name { get; set; }
    }
}