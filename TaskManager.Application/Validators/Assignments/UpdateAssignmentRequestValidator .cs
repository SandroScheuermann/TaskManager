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

            RuleFor(request => request.Description) 
                .Length(1, 500).WithMessage("A descrição deve possuir entre 1 e 500 caracteres."); 

            RuleFor(request => request.Status)
                .IsInEnum().WithMessage("O status deve ser um valor válido do enum AssignmentStatus (0 - Pendente, 1 - Em Progresso, 2 - Finalizado).");

            RuleFor(request => request.Comment) 
                .Length(1, 200).WithMessage("O comentário deve possuir entre 1 e 200 caracteres."); 
        }
    }
}
