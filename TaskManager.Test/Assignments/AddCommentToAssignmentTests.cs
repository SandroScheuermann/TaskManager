using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Handlers.Assignments;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Assignments;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Test.Assignments
{
    public class AddCommentToAssignmentTests
    {
        private Mock<IAssignmentRepository> _assignmentRepository;

        private AddCommentToAssignmentCommandValidator _addCommentToAssignmentValidator;

        private AddCommentToAssignmentHandler _addCommentToAssignmentHandler;

        private AddCommentToAssignmentCommand _addCommentToAssignmentCommand;

        [SetUp]
        public void Setup()
        {
            _assignmentRepository = new();

            _assignmentRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(true);

            _assignmentRepository
                .Setup(repo => repo.AddAssignmentComment(It.IsAny<string>(), It.IsAny<Comment>()))
                .ReturnsAsync(new UpdateResult.Acknowledged(1, 1, It.IsAny<BsonValue>()));

            _addCommentToAssignmentValidator = new();

            _addCommentToAssignmentCommand = new()
            {
                Id = It.IsAny<ObjectId>().ToString(),
                Request = new()
                {
                    Comment = "ComentarioTest",
                    UserId = It.IsAny<ObjectId>().ToString()
                }
            };

            _addCommentToAssignmentHandler = new(_assignmentRepository.Object, _addCommentToAssignmentValidator);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _addCommentToAssignmentHandler.Handle(_addCommentToAssignmentCommand, default);

            response.Error.Should().BeNull();
        }

        [Test]
        public async Task Should_Return_RequestValidationError()
        {
            _addCommentToAssignmentCommand.Request.UserId = "notAndUserId";

            var response = await _addCommentToAssignmentHandler.Handle(_addCommentToAssignmentCommand, default);

            response.Error.Should().BeOfType<RequestValidationError>();
        }

        [Test]
        public async Task Should_Return_AssignmentNotFoundError()
        {
            _assignmentRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(false);

            var response = await _addCommentToAssignmentHandler.Handle(_addCommentToAssignmentCommand, default);

            response.Error.Should().BeOfType<AssignmentNotFoundError>();
        }

        [Test]
        public async Task Should_Return_FailedToAddCommentError()
        { 
            _assignmentRepository
                .Setup(repo => repo.AddAssignmentComment(It.IsAny<string>(), It.IsAny<Comment>()))
                .ReturnsAsync(new UpdateResult.Acknowledged(0, 0, It.IsAny<BsonValue>()));

            var response = await _addCommentToAssignmentHandler.Handle(_addCommentToAssignmentCommand, default);

            response.Error.Should().BeOfType<FailedToAddCommentError>();
        } 
    }
}
