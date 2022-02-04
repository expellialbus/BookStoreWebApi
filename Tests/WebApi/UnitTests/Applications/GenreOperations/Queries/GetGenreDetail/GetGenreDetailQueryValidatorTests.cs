using FluentAssertions;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace UnitTests.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotReturnError()
        { 
            // Since this function tests the validator 
            // the query object's property won't be used
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();

            query.GenreId = 5;  // it does not matter the genre with provided id in database

            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
        
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldReturnError()
        { 
            // Since this function tests the validator 
            // the query object's property won't be used
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();

            query.GenreId = 0; 

            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}