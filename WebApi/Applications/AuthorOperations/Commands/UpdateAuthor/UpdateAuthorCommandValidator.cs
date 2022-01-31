using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            // Validates the AuthorId property of UpdateAuthorCommand class is greater than 0
            RuleFor(command => command.AuthorId).GreaterThan(0);
        }
    }
}