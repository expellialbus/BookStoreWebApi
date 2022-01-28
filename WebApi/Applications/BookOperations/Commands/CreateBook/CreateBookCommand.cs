using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookDtoModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            // Checks database to ensure there is no any book with provided title
            var book = _context.Books.SingleOrDefault(book => book.Title == Model.Title);

            // If there is already a book with the title 
            // an exception will be thrown
            if (book is not null)
                throw new InvalidOperationException("There is already a book with this title in the database.!");

            // If there is not, new book will be written to the database
            book = _mapper.Map<Book>(Model);

            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }

    public class CreateBookDtoModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}