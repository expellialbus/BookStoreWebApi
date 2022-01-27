using AutoMapper;
using FluentAssertions;
using FluentValidation;
using UnitTests.TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using Xunit;

namespace UnitTests.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the query object's properties won't be used
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

            query.BookId = 5; // it does not matter the book with provided is in database

            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        {
            // Since this function tests the validator 
            // the query object's properties won't be used
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

            query.BookId = 0; // it does not matter the book with provided is in database

            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}