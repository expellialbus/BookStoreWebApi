using AutoMapper;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>().ForMember(model => model.Genre,
                options => options.MapFrom(book => book.Genre.Name));
            CreateMap<Book, BookViewModel>().ForMember(model => model.Author,
                options => options.MapFrom(book => book.Author.Name));
            CreateMap<Book, BookViewModel>().ForMember(model => model.PublishDate,
                options => options.MapFrom(book => book.PublishDate.ToString("dd/MM/yyyy")));
        }
    }
}