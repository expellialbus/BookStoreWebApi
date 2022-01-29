using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            // Gets Author specified by AuthorId property
            var author = _context.Authors.SingleOrDefault(author => author.Id == AuthorId);

            // If there is no any author with the specified id 
            // an exception will be thrown
            if (author is null)
                throw new InvalidOperationException("No such author.!");

            return _mapper.Map<AuthorDetailViewModel>(author);
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }
}