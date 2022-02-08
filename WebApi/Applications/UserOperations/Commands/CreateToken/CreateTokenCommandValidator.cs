using FluentValidation;

namespace WebApi.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(command => command.Model.Email.Trim()).NotEmpty();
            RuleFor(command => command.Model.Password.Trim()).NotEmpty();
        }
    }
}