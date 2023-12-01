using FluentValidation;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class UpdateAssignmentRequestValidator : AbstractValidator<UpdateAssignmentRequest>
    {
        public UpdateAssignmentRequestValidator()
        {
            RuleFor(request => request.Id) 
                .MustBeValidObjectId("Assignment ID is not a valid ObjectId")
                .NotEmpty().WithMessage("Assignment ID is required.");   
        }
    }
}
