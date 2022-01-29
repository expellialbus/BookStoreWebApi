using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void AllAuthorsShouldBeReturned()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

            FluentActions
                .Invoking(() => query.Handle())
                .Invoke().Count.Should().Be(3);
        }
    }
}