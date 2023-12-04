using FluentValidation;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class AddCommentToAssignmentCommandValidator : AbstractValidator<AddCommentToAssignmentCommand>
    {
        public AddCommentToAssignmentCommandValidator()
        {
            RuleFor(command => command.Id.ToString())
                .NotEmpty().WithMessage("O ID da tarefa é um campo obrigatório")
                .MustBeValidObjectId("O ID da tarefa não é um ObjectId válido.");

            RuleFor(command => command.Request.UserId.ToString())
                .NotEmpty().WithMessage("O ID do usuário é um campo obrigatório")
                .MustBeValidObjectId("O ID do usuário não é um ObjectId válido.");

            RuleFor(command => command.Request.Comment)
                .NotEmpty().WithMessage("O comentário não pode ser vazio")
                .Length(1, 100).WithMessage("O comentário deve possuir entre 1 e 100 caracteres.");
        }
    }
}
