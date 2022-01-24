using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }

        // Create Books table in the database from Book entity
        public DbSet<Book> Books { get; set; }
        // Create Genres table in the database from Genre entity
        public DbSet<Genre> Genres { get; set; }
        // Create Authors table in the database from Author entity
        public DbSet<Author> Authors { get; set; }
        // Create Users table in the database from User entity
        public DbSet<User> Users { get; set; }

        // Overrides SaveChanges method implemented from IBookStoreDbContext interface
        // and returns result of SaveChanges method of DbContext base class
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}