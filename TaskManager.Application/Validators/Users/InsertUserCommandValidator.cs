using FluentValidation;
using TaskManager.Application.Commands.Users;

namespace TaskManager.Application.Validators.Projects
{
    public class InsertUserCommandValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserCommandValidator()
        {
            RuleFor(command => command.Request.UserRole)
                .IsInEnum().WithMessage("O cargo do usuário não é válido."); 

            RuleFor(command => command.Request.UserName)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório")
                .Length(1, 30).WithMessage("O nome do usuário deve possuir entre 1 e 30 caracteres.");
        }
    }
}
