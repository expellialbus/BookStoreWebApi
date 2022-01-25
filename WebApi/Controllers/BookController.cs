using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Creates an instance of GetBooksQuery class and returns all books to the client
            // Returns 200 OK with list of books if there is any data in the books table
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Creates and instance of GetBookDetailQuery and returns book specified by id 
            // Return 200 OK if there is the specified book in the database
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;

            var result = query.Handle();

            return Ok(result);
        }
    }
}