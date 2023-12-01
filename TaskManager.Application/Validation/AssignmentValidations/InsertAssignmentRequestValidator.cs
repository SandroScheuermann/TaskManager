using FluentValidation;
using TaskManager.Application.Requests.AssignmentRequests;

namespace TaskManager.Application.Validation.AssignmentValidations
{
    public class InsertAssignmentRequestValidator : AbstractValidator<CreateAssignmentRequest>
    {
        public InsertAssignmentRequestValidator()
        {
            RuleFor(request => request.ProjectId)
                .NotEmpty().WithMessage("O ID do projeto é obrigatório.");

            RuleFor(request => request.Title)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .Length(1, 100).WithMessage("O título deve ter entre 1 e 100 caracteres.");

            RuleFor(request => request.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .Length(1, 500).WithMessage("A descrição deve ter entre 1 e 500 caracteres.");

            RuleFor(request => request.ExpireDate)
                .GreaterThan(DateTime.Now).WithMessage("A data de expiração deve ser no futuro.");

            RuleFor(request => request.Status)
                .IsInEnum().WithMessage("O status deve ser um valor válido do enum AssignmentStatus.");
        }
    }
}
