using FluentValidation;
using TaskManager.Application.Requests.Users;

namespace TaskManager.Application.Validators.Projects
{
    public class InsertUserRequestValidator : AbstractValidator<InsertUserRequest>
    {
        public InsertUserRequestValidator()
        {
            RuleFor(request => request.UserRole)
                .IsInEnum().WithMessage("O cargo do usuário não é válido."); 

            RuleFor(request => request.UserName)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório")
                .Length(1, 30).WithMessage("O nome do usuário deve possuir entre 1 e 30 caracteres.");
        }
    }
}
