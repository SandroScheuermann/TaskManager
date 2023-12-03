using FluentValidation;
using TaskManager.Application.Requests.Users;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Users
{
    public class GetUserProjectsRequestValidator : AbstractValidator<GetUserProjectsRequest>
    {
        public GetUserProjectsRequestValidator()
        {
            RuleFor(request => request.UserId)
                .NotEmpty().WithMessage("O ID do usuário é um campo obrigatório")
                .MustBeValidObjectId("O ID do usuário não é um ObjectId válido.");
        }
    }
}
