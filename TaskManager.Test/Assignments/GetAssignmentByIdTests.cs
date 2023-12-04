using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Handlers.Assignments;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Assignments;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Test.Assignments
{
    public class GetAssignmentByIdTests
    {
        private Mock<IAssignmentRepository> _assignmentRepository;

        private GetAssignmentByIdHandler _insertAssignmentHandler;

        private GetAssignmentByIdCommand _insertAssignmentCommand;

        private GetAssignmentByIdCommandValidator _insertAssignmentValidator;

        [SetUp]
        public void Setup()
        {
            _assignmentRepository = new();

            _insertAssignmentValidator = new();

            _assignmentRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new Assignment 
                {
                    Id = It.IsAny<ObjectId>().ToString(),
                    ProjectId = It.IsAny<ObjectId>().ToString(),
                    Description = It.IsAny<string>(),
                    ExpirationDate = It.IsAny<DateTime>(),
                    Priority = It.IsAny<AssignmentPriorityEnum>(),
                    Title = It.IsAny<string>(),
                    Status = It.IsAny<AssignmentStatusEnum>(),
                }); 

            _insertAssignmentCommand = new() { Id = It.IsAny<ObjectId>().ToString() };

            _insertAssignmentHandler = new(_assignmentRepository.Object, _insertAssignmentValidator);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeNull();
        }

        [Test]
        public async Task Should_Return_RequestValidationError()
        {
            _insertAssignmentCommand.Id = "notAnObjectId";

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<RequestValidationError>();
        }

        [Test]
        public async Task Should_Return_AssignmentNotFoundError()
        {
            _assignmentRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(It.Is<Assignment>(x => x == null));

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<AssignmentNotFoundError>();
        }
    }
}
