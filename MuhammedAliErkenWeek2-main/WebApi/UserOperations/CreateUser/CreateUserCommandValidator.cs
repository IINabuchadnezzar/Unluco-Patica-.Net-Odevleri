using FluentValidation;

namespace WebApi.UserOperations.CreateUser
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Email).EmailAddress();
            RuleFor(command => command.Model.FirstName).NotEmpty();
            RuleFor(command => command.Model.LastName).NotEmpty();
        }
    }
}