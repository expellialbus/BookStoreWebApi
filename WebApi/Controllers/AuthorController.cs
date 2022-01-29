using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Creates an instance of GetAuthorDetailQuery class and returns author specified by id
            // Returns 200 OK if there is the specified author in the authors table
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);

            query.AuthorId = id;
            
            var result = query.Handle();

            return Ok(result);
        }
    }
}