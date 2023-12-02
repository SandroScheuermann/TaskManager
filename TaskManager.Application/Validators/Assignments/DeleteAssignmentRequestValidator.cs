using FluentValidation;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class DeleteAssignmentRequestValidator : AbstractValidator<DeleteAssignmentRequest>
    {
        public DeleteAssignmentRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("O ID da tarefa é um campo obrigatório")
                .MustBeValidObjectId("O ID da tarefa não é um ObjectId válido.");
        }
    }
}
