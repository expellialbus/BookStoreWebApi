using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Common;
using WebApi.DbOperations;

namespace UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public IBookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set; }

        public CommonTestFixture()
        {
            
            // Builds options for BookStoreDbContext 
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            
            // Creates context
            var context = new BookStoreDbContext(options);
            
            // Ensures database created
            context.Database.EnsureCreated();
            
            // Add test data to the database
            context.AddBooks();
            context.AddGenres();
            context.AddAuthors();

            context.SaveChanges();

            Context = context;
            
            // Creates a mapper object with the configurations of MappingProfile class
            Mapper = new MapperConfiguration(expression => expression.AddProfile<MappingProfile>()).CreateMapper();

            // Creates a new configuration
            Configuration = new TokenConfiguration();
        }
    }
}