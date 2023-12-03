using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Assignments
{
    public class UpdateAssignmentHandler(IAssignmentRepository assignmentRepository, IValidator<UpdateAssignmentRequest> assignmentValidator)
        : IRequestHandler<UpdateAssignmentCommand, Result<UpdateAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<UpdateAssignmentRequest> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<UpdateAssignmentResponse, Error>> Handle(UpdateAssignmentCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command.Request)
                .Bind(CheckAssignmentExistance)
                .Bind(UpdateAssignment);

            return Task.FromResult(response);
        } 

        private Result<UpdateAssignmentRequest, Error> ValidateRequest(UpdateAssignmentRequest request)
        {
            var validationResult = AssignmentValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        private Result<UpdateAssignmentRequest, Error> CheckAssignmentExistance(UpdateAssignmentRequest request)
        {
            var assignmentExists = AssignmentRepository.CheckExistanceById(request.Id).Result;

            if (!assignmentExists)
            {
                return new AssignmentDoesntExistError();
            }

            return request;
        }
        private Result<UpdateAssignmentResponse, Error> UpdateAssignment(UpdateAssignmentRequest request)
        {
            var updatedAssignment = new Assignment()
            {
                Id = request.Id,
                ProjectId = string.Empty,
                Title = string.Empty,
                Description = request.Description,
                ExpirationDate = null,
                Status = request.Status, 
                Priority = null,
            };

            var result = AssignmentRepository.UpdateAsync(updatedAssignment).Result;

            if (result.ModifiedCount < 0)
            {
                return new UnknownError();
            }

            var response = new UpdateAssignmentResponse()
            {
                Id = updatedAssignment.Id, 
            };

            return response;
        }
    }
}
