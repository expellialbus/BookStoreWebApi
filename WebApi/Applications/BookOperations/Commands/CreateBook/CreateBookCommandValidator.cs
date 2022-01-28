using System;
using FluentValidation;

namespace WebApi.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            // Validates the Title property of CreateBookDtoModel class is not empty
            RuleFor(command => command.Model.Title.Trim()).NotEmpty();
    
            // Validates the GenreId property of CreateBookDtoModel class is greater than 0
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            
            // Validates the AuthorId property of CreateBookDtoModel class is greater than 0
            RuleFor(command => command.Model.AuthorId).GreaterThan(0);
            
            // Validates the PageCount property of CreateBookDtoModel class is greater than 0
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            
            // Validates the PublishDate property of CreateBookDtoModel class is less than or equal to now
            RuleFor(command => command.Model.PublishDate).LessThanOrEqualTo(DateTime.Now);
        }
    }
}