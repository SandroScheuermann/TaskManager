using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Application.Handlers.Assignments
{
    public class DeleteAssignmentHandler(IAssignmentRepository assignmentRepository, IValidator<DeleteAssignmentCommand> assignmentValidator)
        : IRequestHandler<DeleteAssignmentCommand, Result<DeleteAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<DeleteAssignmentCommand> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<DeleteAssignmentResponse, Error>> Handle(DeleteAssignmentCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
                .Bind(CheckAssignmentExistance)
                .Bind(DeleteAssignment);

            return Task.FromResult(response);
        }

        private Result<DeleteAssignmentCommand, Error> ValidateRequest(DeleteAssignmentCommand command)
        {
            var validationResult = AssignmentValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<DeleteAssignmentCommand, Error> CheckAssignmentExistance(DeleteAssignmentCommand command)
        {
            var assignmentExists = AssignmentRepository.CheckExistanceById(command.Id).Result;

            if (!assignmentExists)
            {
                return new AssignmentDoesntExistError();
            }

            return command;
        }
        private Result<DeleteAssignmentResponse, Error> DeleteAssignment(DeleteAssignmentCommand command)
        {
            var result = AssignmentRepository.DeleteAsync(command.Id).Result;

            return result.DeletedCount > 0 ? new DeleteAssignmentResponse { } : new UnknownError("Falha ao deletar a tarefa informada.");
        }
    }
}
