using FluentValidation;

namespace WebApi.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            // Validates the BookId property of DeleteBookCommand class is greater than 0
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}