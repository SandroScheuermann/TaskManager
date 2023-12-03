using FluentValidation;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class UpdateAssignmentCommandValidator : AbstractValidator<UpdateAssignmentCommand>
    {
        public UpdateAssignmentCommandValidator()
        {
            RuleFor(command => command.Id.ToString())
                .NotEmpty().WithMessage("O ID da tarefa é um campo obrigatório")
                .MustBeValidObjectId("O ID da tarefa não é um ObjectId válido.");

            RuleFor(command => command.Request.Description)
                .Length(1, 500).WithMessage("A descrição deve possuir entre 1 e 500 caracteres.");

            RuleFor(command => command.Request.Status)
                .IsInEnum().WithMessage("O status deve ser um valor válido do enum AssignmentStatus (0 - Pendente, 1 - Em Progresso, 2 - Finalizado).");
        }
    }
}
