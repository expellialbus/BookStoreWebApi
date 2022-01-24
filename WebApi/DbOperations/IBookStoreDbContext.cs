using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public interface IBookStoreDbContext
    {
        // Create Books table in the database from Book entity
        public DbSet<Book> Books { get; set; }
        // Create Genres table in the database from Genre entity
        public DbSet<Genre> Genres { get; set; }
        // Create Authors table in the database from Author entity
        public DbSet<Author> Authors { get; set; }
        // Create Users table in the database from User entity
        public DbSet<User> Users { get; set; }

        public int SaveChanges();
    }
}