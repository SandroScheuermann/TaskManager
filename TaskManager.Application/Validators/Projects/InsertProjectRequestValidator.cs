using FluentValidation;
using TaskManager.Application.Requests.Projects;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Projects
{
    public class InsertProjectRequestValidator : AbstractValidator<InsertProjectRequest>
    {
        public InsertProjectRequestValidator()
        {
            RuleFor(request => request.UserId.ToString())
                .NotEmpty().WithMessage("O ID do usuário é um campo obrigatório")
                .MustBeValidObjectId("O ID do usuário não é um ObjectId válido.");

            RuleFor(request => request.ProjectName)
                .NotEmpty().WithMessage("O nome do projeto é obrigatório")
                .Length(1, 30).WithMessage("O nome do projeto deve possuir entre 1 e 30 caracteres.");
        }
    }
}
