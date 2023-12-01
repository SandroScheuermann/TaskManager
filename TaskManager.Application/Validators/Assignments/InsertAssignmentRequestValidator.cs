using FluentValidation;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Validators.Shared;

namespace TaskManager.Application.Validators.Assignments
{
    public class InsertAssignmentRequestValidator : AbstractValidator<CreateAssignmentRequest>
    {
        public InsertAssignmentRequestValidator()
        {
            RuleFor(request => request.ProjectId)
                .NotEmpty().WithMessage("Project ID is required.")
                .MustBeValidObjectId("Project ID is not a valid ObjectId");

            RuleFor(request => request.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");

            RuleFor(request => request.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");

            RuleFor(request => request.ExpireDate)
                .GreaterThan(DateTime.Now).WithMessage("Expiration date must be in the future.");

            RuleFor(request => request.Status)
                .IsInEnum().WithMessage("Status must be a valid value from the AssignmentStatus enum.");
        }
    }
}
