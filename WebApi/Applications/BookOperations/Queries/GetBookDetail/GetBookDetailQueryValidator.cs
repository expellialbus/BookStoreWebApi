using FluentValidation;

namespace WebApi.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            // Validates the BookId property of GetBookDetailQuery class is greater than 0
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}