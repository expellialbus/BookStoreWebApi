using System;
using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Genre_ShouldBeReturned()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);

            query.GenreId = 1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().NotBeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);

            query.GenreId = 5; // there is no genre with id 5 in the provided database

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("No such genre.!");
        }
    }
}