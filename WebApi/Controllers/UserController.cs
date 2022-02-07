using AutoMapper;
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

            command.Model = model;
            
            command.Handle();

            return Ok();
        }
    }
}