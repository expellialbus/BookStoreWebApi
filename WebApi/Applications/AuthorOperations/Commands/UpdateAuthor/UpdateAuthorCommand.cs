using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorDtoModel Model { get; set; }

        private readonly IBookStoreDbContext _context;

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {  
            // Checks if there is an author with provided id in the database
            var author = _context.Authors.SingleOrDefault(author => author.Id == AuthorId);

            // An exception will be thrown if there is not
            if (author is null)
                throw new InvalidOperationException("No such author.!");

            // Author will be updated if the inputs are not default values
            author.Name = Model.Name.Trim() != "" ? Model.Name : author.Name;
            author.Surname = Model.Surname.Trim() != "" ? Model.Surname : author.Surname;
            
            // A DateTime greater than or equal to now should be inputted
            // to keep the BirthDate property unchanged while updating author
            author.BirthDate = Model.BirthDate < DateTime.Now ? Model.BirthDate : author.BirthDate;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorDtoModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}