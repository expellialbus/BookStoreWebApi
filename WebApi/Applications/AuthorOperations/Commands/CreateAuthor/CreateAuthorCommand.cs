using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorDtoModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            // Checks if there is already an author with provided name and surname in the database
            var author = _context.Authors
                .FirstOrDefault(author => author.Name == Model.Name && author.Surname == Model.Surname);

            // An exception will be thrown if there is
            if (author is not null)
                throw new InvalidOperationException("The author is already in database");

            author = _mapper.Map<Author>(Model);
            
            // or the author will be added to the database if there is not
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorDtoModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}