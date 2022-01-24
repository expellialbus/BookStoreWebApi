using WebApi.DbOperations;
using WebApi.Entities;

namespace UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this IBookStoreDbContext context)
        {
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
        }
    }
}