using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            // Gets all authors from the database
            var authors = _context.Authors.OrderBy(author => author.Id);

            // Automatically maps Author objects to AuthorViewModel object and returns
            return _mapper.Map<List<AuthorViewModel>>(authors);
        }
    }
    
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }
}