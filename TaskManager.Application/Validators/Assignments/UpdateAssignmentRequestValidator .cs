using FluentValidation;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class UpdateAssignmentRequestValidator : AbstractValidator<UpdateAssignmentRequest>
    {
        public UpdateAssignmentRequestValidator()
        {
            RuleFor(request => request.Id.ToString())
                .NotEmpty().WithMessage("O ID da tarefa é obrigatório.")
                .MustBeValidObjectId("O ID da tarefa não é um ObjectId válido.");
        }
    }
}
