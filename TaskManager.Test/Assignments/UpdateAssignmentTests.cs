using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Handlers.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Assignments;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Test.Assignments
{
    public class UpdateAssignmentTests
    {
        private Mock<IAssignmentRepository> _assignmentRepository;

        private UpdateAssignmentCommandValidator _updateAssignmentValidator;

        private UpdateAssignmentHandler _updateAssignmentHandler;

        private UpdateAssignmentCommand _updateAssignmentCommand;

        private UpdateAssignmentRequest _updateAssignmentRequest;

        [SetUp]
        public void Setup()
        {
            _assignmentRepository = new();

            _assignmentRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(true);

            _assignmentRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Assignment>()))
                .ReturnsAsync(new UpdateResult.Acknowledged(1, 1, It.IsAny<BsonValue>()));

            _updateAssignmentValidator = new();

            _updateAssignmentRequest = new UpdateAssignmentRequest
            {
                UserId = It.IsAny<ObjectId>().ToString(),
                Description = "descricaoTeste",
                Status = It.IsAny<AssignmentStatusEnum>(),
            };

            _updateAssignmentCommand = new UpdateAssignmentCommand
            {
                Id = It.IsAny<ObjectId>().ToString(),
                Request = _updateAssignmentRequest,
            };

            _updateAssignmentHandler = new(_assignmentRepository.Object, _updateAssignmentValidator);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _updateAssignmentHandler.Handle(_updateAssignmentCommand, default);

            response.Error.Should().BeNull();
        }

        [Test]
        public async Task Should_Return_RequestValidationError()
        {
            _updateAssignmentCommand.Request.Description = new string('a', 1000);

            var response = await _updateAssignmentHandler.Handle(_updateAssignmentCommand, default);

            response.Error.Should().BeOfType<RequestValidationError>();
        }

        [Test]
        public async Task Should_Return_AssignmentNotFoundError()
        {
            _assignmentRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(false);

            var response = await _updateAssignmentHandler.Handle(_updateAssignmentCommand, default);

            response.Error.Should().BeOfType<AssignmentNotFoundError>();
        }

        [Test]
        public async Task Should_Return_FailedToUpdateError()
        {
            _assignmentRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Assignment>()))
                .ReturnsAsync(new UpdateResult.Acknowledged(0, 0, It.IsAny<BsonValue>()));

            var response = await _updateAssignmentHandler.Handle(_updateAssignmentCommand, default);

            response.Error.Should().BeOfType<FailedToUpdateError>();
        }
    }
}
