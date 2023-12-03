using FluentValidation;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class GetAssignmentByIdCommandValidator : AbstractValidator<GetAssignmentByIdCommand>
    {
        public GetAssignmentByIdCommandValidator()
        {
            RuleFor(command => command.Id.ToString())
                .NotEmpty().WithMessage("O ID da tarefa é um campo obrigatório")
                .MustBeValidObjectId("O ID da tarefa não é um ObjectId válido.");
        }
    }
}
