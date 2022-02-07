using FluentValidation;

namespace WebApi.Applications.UserOperations.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name.Trim()).NotEmpty();
            RuleFor(command => command.Model.Surname.Trim()).NotEmpty();
            RuleFor(command => command.Model.Email.Trim()).NotEmpty();
            RuleFor(command => command.Model.Password.Trim()).NotEmpty();
        }
    }
}