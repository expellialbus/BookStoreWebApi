using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.BookOperations.Queries.GetBooks
{
    public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void AllBooksShouldBeReturned()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);

            FluentActions
                .Invoking(() => query.Handle())
                .Invoke().Count.Should().Be(3);
        }
    }
}