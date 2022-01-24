using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Applications.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            // Gets all books from Books table in the database
            var books = _context.Books
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .OrderBy(book => book.Id).ToList();

            // Automatically maps Book objects to BookViewModel object and returns
            return _mapper.Map<List<BookViewModel>>(books);
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}