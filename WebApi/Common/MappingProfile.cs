using AutoMapper;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Book();
            Author();
            Genre();
        }
        
        // Does map operations for Book entity
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

            CreateMap<CreateBookDtoModel, Book>();
        }

        // Does map operations for Author entity
        private void Author()
        {
            CreateMap<Author, AuthorViewModel>()
                .ForMember(model => model.BirthDate, // Maps Author.BirthDate to BirthDate property of AuthorViewModel
                    options => options.MapFrom(author => author.BirthDate.ToString("dd/MM/yyyy")));
            
            CreateMap<Author, AuthorDetailViewModel>()
                .ForMember(model => model.BirthDate, // Maps Author.BirthDate to BirthDate property of AuthorDetailViewModel
                    options => options.MapFrom(author => author.BirthDate.ToString("dd/MM/yyyy")));

            CreateMap<CreateAuthorDtoModel, Author>();
        }
        
        // Does map operations for Genre entity
        private void Genre()
        {
            CreateMap<Genre, GenreViewModel>();
        }
    }
}