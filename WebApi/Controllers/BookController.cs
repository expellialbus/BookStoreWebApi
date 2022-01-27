using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
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
            // Creates an instance of GetBooksQuery class and returns all books
            // Returns 200 OK if there is any data in the books table
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Creates and instance of GetBookDetailQuery and returns book specified by id 
            // Returns 200 OK if there is the specified book in the books table
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

            query.BookId = id;

            // Tries to validate GetBookDetailQuery class properties
            // according to rules provided in GetBookDetailQueryValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(query);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Creates and instance of DeleteBookCommand and deletes book specified by id 
            // Returns 200 OK if there is the specified book in the books table
            DeleteBookCommand command = new DeleteBookCommand(_context);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            command.BookId = id;

            // Tries to validate DeleteBookCommand class properties
            // according to rules provided in DeleteBookCommandValidator class constructor
            // If properties are not consistent with the rules 
            // an error will be thrown
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}