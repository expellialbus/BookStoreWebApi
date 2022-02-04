using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            // Gets all genres from Genres table in the database
            var genres = _context.Genres.Where(genre => genre.IsActive == true)
                .OrderBy(genre => genre.Id).ToList();

            return _mapper.Map<List<GenreViewModel>>(genres);
        }
    }

    public class GenreViewModel
    {
        public string Name { get; set; }
    }
}