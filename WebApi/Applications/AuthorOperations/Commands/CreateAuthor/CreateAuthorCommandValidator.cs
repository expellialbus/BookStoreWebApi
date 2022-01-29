using System;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            // Validates the Name property of CreateAuthorDtoModel class is not empty
            RuleFor(command => command.Model.Name.Trim()).NotEmpty();
            
            // Validates the Surname property of CreateAuthorDtoModel class is not empty
            RuleFor(command => command.Model.Surname.Trim()).NotEmpty();
            
            // Validates the BirthDate property of CreateAuthorDtoModel class is less than today
            RuleFor(command => command.Model.BirthDate).LessThan(DateTime.Now.AddYears(-1));
        }
    }
}