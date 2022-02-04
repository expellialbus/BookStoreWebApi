using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            // Gets genre specified by GenreId property
            // The activeness of a genre is not matter when the genre is gotten by id 
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == GenreId);
            
            // An exception will be thrown
            // if there is no a genre with provided id
            if (genre is null)
                throw new InvalidOperationException("No such genre.!");

            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}