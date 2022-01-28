using FluentValidation;

namespace WebApi.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            // Validates the BookId property is greater than 0
            RuleFor(command => command.BookId).GreaterThan(0);

            // Validates the GenreId property of UpdateBookDtoModel is greater than or equal to 0
            RuleFor(command => command.Model.GenreId).GreaterThanOrEqualTo(0);
            
            // Validates the AuthorId property of UpdateBookDtoModel is greater than or equal to 0
            RuleFor(command => command.Model.AuthorId).GreaterThanOrEqualTo(0);
            
            // Validates the GenreId property of UpdateBookDtoModel is greater than or equal to 0
            RuleFor(command => command.Model.PageCount).GreaterThanOrEqualTo(0);
        }
    }
}