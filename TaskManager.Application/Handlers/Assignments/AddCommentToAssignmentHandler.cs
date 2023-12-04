using FluentValidation;
using MediatR;
using MongoDB.Bson;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Application.Handlers.Assignments
{
    public class AddCommentToAssignmentHandler(IAssignmentRepository assignmentRepository, IValidator<AddCommentToAssignmentCommand> assignmentValidator)
        : IRequestHandler<AddCommentToAssignmentCommand, Result<AddCommentToAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<AddCommentToAssignmentCommand> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<AddCommentToAssignmentResponse, Error>> Handle(AddCommentToAssignmentCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
                .Bind(CheckAssignmentExistance)
                .Bind(AddAssignmentComment);

            return Task.FromResult(response);
        }

        private Result<AddCommentToAssignmentCommand, Error> ValidateRequest(AddCommentToAssignmentCommand command)
        {
            var validationResult = AssignmentValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<AddCommentToAssignmentCommand, Error> CheckAssignmentExistance(AddCommentToAssignmentCommand command)
        {
            var assignmentExists = AssignmentRepository.CheckExistanceById(command.Id).Result;

            if (!assignmentExists)
            {
                return new AssignmentDoesntExistError();
            }

            return command;
        }
        private Result<AddCommentToAssignmentResponse, Error> AddAssignmentComment(AddCommentToAssignmentCommand command)
        {
            var comment = new Comment { Content = command.Request.Comment, Id = ObjectId.GenerateNewId().ToString() };

            var updateResponse = AssignmentRepository.AddAssignmentComment(command.Id, comment).Result;

            if (updateResponse.ModifiedCount > 0)
            {
                return new AddCommentToAssignmentResponse { Id = comment.Id };
            }

            return new UnknownError("Erro ao adicionar comentário");
        }
    }
}
