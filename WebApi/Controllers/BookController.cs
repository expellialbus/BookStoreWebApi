using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}