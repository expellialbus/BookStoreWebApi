using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Gets options for BookStoreDbContext class
            // and creates an instance of it to add example data to the database
            var options = serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>();
            var context = new BookStoreDbContext(options);

            // If there is any data in the Books table of database
            // method adds nothing to the database
            if (context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(
                new Book
                {
                    Title = "Herland",
                    GenreId = 2,
                    AuthorId = 2,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2,
                    AuthorId = 3,
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21)
                },
                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1,
                    AuthorId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                }
            );

            context.Genres.AddRange(
                new Genre
                {
                    Name = "Personal Growth"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Romance"
                }
            );

            context.Authors.AddRange(
                new Author
                {
                    Name = "Eric",
                    Surname = "Ries",
                    BirthDate = new DateTime(1978, 9, 22)
                },
                new Author
                {
                    Name = "Charlotte Perkins",
                    Surname = "Gilman",
                    BirthDate = new DateTime(1860, 7, 03)
                },
                new Author
                {
                    Name = "Frank",
                    Surname = "Herbert",
                    BirthDate = new DateTime(1986, 02, 11)
                }
            );
            
            // Saves changes to the database
            context.SaveChanges();
        }
    }
}