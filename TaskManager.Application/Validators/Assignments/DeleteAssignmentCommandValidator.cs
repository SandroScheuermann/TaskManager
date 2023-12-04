using FluentValidation;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class DeleteAssignmentCommandValidator : AbstractValidator<DeleteAssignmentCommand>
    {
        public DeleteAssignmentCommandValidator()
        {
            RuleFor(command => command.Id.ToString())
                .NotEmpty().WithMessage("O ID da tarefa é um campo obrigatório")
                .MustBeValidObjectId("O ID da tarefa não é um ObjectId válido.");

            RuleFor(command => command.UserId.ToString())
                .NotEmpty().WithMessage("O ID do usuário é um campo obrigatório")
                .MustBeValidObjectId("O ID do usuário não é um ObjectId válido.");
        }
    }
}
