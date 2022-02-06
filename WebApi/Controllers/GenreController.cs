using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
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
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();

            query.GenreId = id;

            // Tries to validate GetGenreDetailQuery class properties
            // according to rules provided in GetGenreDetailQueryValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(query);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateGenreDtoModel model)
        {
            // Creates and instance of CreateGenreCommand and adds new genre to the database
            // Returns 200 OK if the genre is successfully added to the database
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            command.Model = model;

            // Tries to validate CreateGenreCommand class properties
            // according to rules provided in CreateGenreCommandValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] UpdateGenreDtoModel model, int id)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            command.GenreId = id;
            command.Model = model;

            // Tries to validate UpdateGenreCommand class properties
            // according to rules provided in UpdateGenreCommandValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Creates and instance of DeleteGenreCommand and deletes the specified by id
            // Returns 200 OK if the genre is successfully deleted from database   
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            command.GenreId = id;

            // Tries to validate DeleteGenreCommand class properties
            // according to rules provided in DeleteGenreCommandValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}