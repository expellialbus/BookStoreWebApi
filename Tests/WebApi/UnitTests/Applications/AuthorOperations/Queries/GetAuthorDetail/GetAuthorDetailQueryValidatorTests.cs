using FluentAssertions;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace UnitTests.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Since this function tests the validator 
            // the query object's properties won't be used
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();

            query.AuthorId = 5; // it does not matter the author with provided is in database

            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
        
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        {
            // Since this function tests the validator 
            // the query object's properties won't be used
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();

            query.AuthorId = 0;

            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}