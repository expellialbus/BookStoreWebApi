using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenresQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenIsActiveTrue_Genre_ShouldBeReturned()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);

            FluentActions
                .Invoking(() => query.Handle())
                .Invoke().Count.Should().Be(3);
        }

        [Fact]
        public void WhenIsActiveFalse_Genre_ShouldNotBeReturned()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            
            // Since there is not any inactive genre
            // an inactive genre should be added

            Genre genre = new Genre
            {
                Name = "Test Genre",
                IsActive = false
            };

            _context.Genres.Add(genre);

            FluentActions
                .Invoking(() => query.Handle())
                .Invoke().Count.Should().Be(3);
        }
    }
}