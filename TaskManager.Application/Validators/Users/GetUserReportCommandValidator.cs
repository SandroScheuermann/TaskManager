using FluentValidation;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Users
{
    public class GetUserReportCommandValidator : AbstractValidator<GetUserReportCommand>
    {
        public GetUserReportCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("O ID do usuário é um campo obrigatório")
                .MustBeValidObjectId("O ID do usuário não é um ObjectId válido.");
        }
    }
}
