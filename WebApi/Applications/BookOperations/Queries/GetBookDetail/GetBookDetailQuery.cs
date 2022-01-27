using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            // Gets book specified by BookId property
            var book = _context.Books
                .Include(book => book.Genre)
                .Include(book => book.Author)
                .SingleOrDefault(book => book.Id == BookId);

            // Throws error if there is no such book
            if (book is null)
                throw new InvalidOperationException("No such book.!");

            // Automatically maps Book object to BookDetailViewModel object and returns
            return _mapper.Map<BookDetailViewModel>(book);
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}