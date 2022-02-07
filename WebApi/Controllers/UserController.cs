using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Applications.UserOperations.Commands;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDtoModel model)
        {
            // Creates and instance of CreateUserCommand and adds new user to the database
            // Returns 200 OK if the user is successfully added to the database
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            CreateUserCommandValidator validator = new CreateUserCommandValidator();

            command.Model = model;
            
            // Tries to validate CreateUserCommand class properties
            // according to rules provided in CreateUserCommandValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}