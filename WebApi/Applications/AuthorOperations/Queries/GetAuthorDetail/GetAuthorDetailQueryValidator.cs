using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            // Validates the AuthorId property of GetAuthorDetailQuery class is greater than 0
            RuleFor(query => query.AuthorId).GreaterThan(0);
        }
    }
}