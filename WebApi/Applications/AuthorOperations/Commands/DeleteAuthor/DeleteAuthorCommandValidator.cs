using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            // Validates the AuthorId property of DeleteAuthorCommand class is greater than 0
            RuleFor(command => command.AuthorId).GreaterThan(0);
        }
    }
}