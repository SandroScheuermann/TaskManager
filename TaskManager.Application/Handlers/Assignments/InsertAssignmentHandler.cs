using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Assignments
{
    public class InsertAssignmentHandler(IAssignmentRepository assignmentRepository, IProjectRepository projectRepository, IValidator<InsertAssignmentRequest> assignmentValidator)
        : IRequestHandler<InsertAssignmentCommand, Result<InsertAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;
        public IValidator<InsertAssignmentRequest> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<InsertAssignmentResponse, Error>> Handle(InsertAssignmentCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command.Request)
               .Bind(CheckProjectExistance)
               .Bind(CheckProjectAssignmentsCount)
               .Bind(CreateAndInsertAssignment);

            return Task.FromResult(response);
        }

        private Result<InsertAssignmentRequest, Error> ValidateRequest(InsertAssignmentRequest request)
        {
            var validationResult = AssignmentValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        private Result<InsertAssignmentRequest, Error> CheckProjectExistance(InsertAssignmentRequest request)
        {
            var projectExist = ProjectRepository.CheckExistanceById(request.ProjectId).Result;

            if (!projectExist)
            {
                return new ProjectDoesntExistError();
            }

            return request;
        }
        private Result<InsertAssignmentRequest, Error> CheckProjectAssignmentsCount(InsertAssignmentRequest request)
        {
            var linkedAssignmentsCount = AssignmentRepository.GetAssignmentsCountByProjectId(request.ProjectId).Result;

            if (linkedAssignmentsCount >= 20)
            {
                return new MaximumNumberOfAssignmentsError();
            }

            return request;
        }
        private Result<InsertAssignmentResponse, Error> CreateAndInsertAssignment(InsertAssignmentRequest request)
        {
            var assignment = new Assignment()
            {
                Id = string.Empty,
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                ExpirationDate = request.ExpirationDate,
                Status = request.Status,
                Priority = request.Priority,
            };

            AssignmentRepository.InsertAsync(assignment);

            var response = new InsertAssignmentResponse()
            {
                Id = assignment.Id
            };

            return response;
        }
    }
}
