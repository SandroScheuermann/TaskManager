using FluentValidation;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Projects
{
    public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectCommandValidator()
        {
            RuleFor(command => command.Id.ToString())
                .NotEmpty().WithMessage("O ID do projeto é um campo obrigatório")
                .MustBeValidObjectId("O ID do projeto não é um ObjectId válido.");
        }
    }
}
