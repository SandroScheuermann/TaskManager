using FluentValidation;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class InsertAssignmentRequestValidator : AbstractValidator<InsertAssignmentRequest>
    {
        public InsertAssignmentRequestValidator()
        {
            RuleFor(request => request.ProjectId.ToString())
                .NotEmpty().WithMessage("O ID do projeto é um campo obrigatório.")
                .MustBeValidObjectId("O ID do projeto não é um ObjectId válido..");

            RuleFor(request => request.Title)
                .NotEmpty().WithMessage("O título é um campo obrigatório.")
                .Length(1, 100).WithMessage("O título deve possuir entre 1 e 100 caracteres.");

            RuleFor(request => request.Description)
                .NotEmpty().WithMessage("A descrição é um campo obrigatório.")
                .Length(1, 500).WithMessage("A descrição deve possuir entre 1 e 500 caracteres.");

            RuleFor(request => request.ExpirationDate)
                .GreaterThan(DateTime.Now).WithMessage("A data de expiração deve ser no futuro.");

            RuleFor(request => request.Status)
                .IsInEnum().WithMessage("O status deve ser um valor válido do enum AssignmentStatus (0 - Pendente, 1 - Em Progresso, 2 - Finalizado).");
        }
    }
}
