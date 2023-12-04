using FluentValidation;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class GetProjectAssignmentsCommandValidator : AbstractValidator<GetProjectAssignmentsCommand>
    {
        public GetProjectAssignmentsCommandValidator()
        {
            RuleFor(command => command.ProjectId.ToString())
                .NotEmpty().WithMessage("O ID da tarefa é um campo obrigatório")
                .MustBeValidObjectId("O ID da tarefa não é um ObjectId válido.");
        }
    }
}
