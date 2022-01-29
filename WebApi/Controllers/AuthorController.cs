using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Creates an instance of GetAuthorsQuery class and returns all authors
            // Returns 200 OK if there is any data in the authors table
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

            var result = query.Handle();

            return Ok(result);
        }
    }
}