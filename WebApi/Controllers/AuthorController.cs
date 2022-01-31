using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
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
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            
            query.AuthorId = id;
            
            // Tries to validate GetAuthorDetailQuery class properties
            // according to rules provided in GetAuthorDetailQueryValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAuthorDtoModel model)
        {
            // Creates an instance of CreateAuthorCommand class and adds new author to the database
            // Returns 200 OK if the author successfully added to the database
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            
            command.Model = model;
            
            // Tries to validate CreateAuthorCommand class properties
            // according to rules provided in CreateAuthorCommandValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] UpdateAuthorDtoModel model, int id)
        {
            // Creates an instance of UpdateAuthorCommand class and updates author specified by id
            // Returns 200 OK if the author successfully updated
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

            command.AuthorId = id;
            command.Model = model;
            
            // Tries to validate UpdateAuthorCommand class properties
            // according to rules provided in UpdateAuthorCommandValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}