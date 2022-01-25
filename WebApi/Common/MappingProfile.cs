using AutoMapper;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Book();
        }
        
        // Maps Book objects to BookViewModel according to specified rules
        private void Book()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(model => model.Genre, // Maps Genre.Name to Genre property of BookViewModel
                options => options.MapFrom(book => book.Genre.Name))
                .ForMember(model => model.Author, // Maps Author.Name and Author.Surname to Author property of BookViewModel
                options => options.MapFrom(book => book.Author.Name + " " + book.Author.Surname))
                .ForMember(model => model.PublishDate, // Maps Book.PublishDate to PublishDate property of BookViewModel as string
                options => options.MapFrom(book => book.PublishDate.ToString("dd/MM/yyyy")));

            CreateMap<Book, BookDetailViewModel>()
                .ForMember(model => model.Genre, // Maps Genre.Name to Genre property of BookDetailViewModel
                    options => options.MapFrom(book => book.Genre.Name))
                .ForMember(model => model.Author, // Maps Author.Name and Author.Surname to Author property of BookDetailViewModel
                    options => options.MapFrom(book => book.Author.Name + " " + book.Author.Surname))
                .ForMember(model => model.PublishDate, // Maps Book.PublishDate to PublishDate property of BookDetailViewModel as string
                    options => options.MapFrom(book => book.PublishDate.ToString("dd/MM/yyyy")));
        }
    }
}