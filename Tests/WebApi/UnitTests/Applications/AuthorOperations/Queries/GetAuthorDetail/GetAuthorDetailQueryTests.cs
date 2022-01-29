using System;
using AutoMapper;
using FluentAssertions;
using UnitTests.TestSetup;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Author_ShouldBeReturned()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);

            query.AuthorId = 1;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().NotBeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);

            query.AuthorId = 5; // there is no such author with id 5 in the provided database

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("No such author.!");
        }
    }
}