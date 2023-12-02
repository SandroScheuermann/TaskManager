using FluentValidation;
using TaskManager.Application.Requests.Projects;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Projects
{
    public class DeleteProjectRequestValidator : AbstractValidator<DeleteProjectRequest>
    {
        public DeleteProjectRequestValidator()
        {
            RuleFor(request => request.Id.ToString())
                .NotEmpty().WithMessage("O ID do projeto é um campo obrigatório")
                .MustBeValidObjectId("O ID do projeto não é um ObjectId válido.");
        }
    }
}
