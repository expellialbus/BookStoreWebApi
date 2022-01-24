using AutoMapper;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            BookMaps();
        }

        // Maps Book objects to BookViewModel according to specified rules
        private void BookMaps()
        {
            // Maps Genre.Name to Genre property of BookViewModel
            CreateMap<Book, BookViewModel>().ForMember(model => model.Genre,
                options => options.MapFrom(book => book.Genre.Name));

            // Maps Author.Name and Author.Surname to Author property of BookViewModel
            CreateMap<Book, BookViewModel>().ForMember(model => model.Author,
                options => options.MapFrom(book => book.Author.Name + " " + book.Author.Surname));

            // Maps Book.PublishDate to PublishDate property of BookViewModel as string
            CreateMap<Book, BookViewModel>().ForMember(model => model.PublishDate,
                options => options.MapFrom(book => book.PublishDate.ToString("dd/MM/yyyy")));
        }
    }
}