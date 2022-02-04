using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Creates an instance of GetGenresQuery class and returns all genres
            // Returns 200 OK if there is any data in the genres table
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Creates and instance of GetGenreDetailQuery and returns genre specified by id 
            // Returns 200 OK if there is the specified genre in the genres table
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);

            query.GenreId = id;

            var result = query.Handle();

            return Ok(result);
        }
    }
}