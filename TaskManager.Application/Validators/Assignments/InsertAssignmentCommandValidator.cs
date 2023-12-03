using FluentValidation;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class InsertAssignmentCommandValidator : AbstractValidator<InsertAssignmentCommand>
    {
        public InsertAssignmentCommandValidator()
        {
            RuleFor(command => command.Request.ProjectId.ToString())
                .NotEmpty().WithMessage("O ID do projeto é um campo obrigatório.")
                .MustBeValidObjectId("O ID do projeto não é um ObjectId válido..");

            RuleFor(command => command.Request.Title)
                .NotEmpty().WithMessage("O título é um campo obrigatório.")
                .Length(1, 100).WithMessage("O título deve possuir entre 1 e 100 caracteres.");

            RuleFor(command => command.Request.Description)
                .NotEmpty().WithMessage("A descrição é um campo obrigatório.")
                .Length(1, 500).WithMessage("A descrição deve possuir entre 1 e 500 caracteres.");

            RuleFor(command => command.Request.ExpirationDate)
                .GreaterThan(DateTime.Now).WithMessage("A data de expiração deve ser no futuro.");

            RuleFor(command => command.Request.Status)
                .IsInEnum().WithMessage("O status deve ser um valor válido do enum AssignmentStatus (0 - Pendente, 1 - Em Progresso, 2 - Finalizado).");
           
            RuleFor(command => command.Request.Priority)
                .IsInEnum().WithMessage("A prioridade deve ser um valor válido do enum AssignmentPriorityEnum (0 - Baixo, 1 - Normal, 2 - Alto).");
        }
    }
}
