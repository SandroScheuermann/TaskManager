using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Handlers.Assignments;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Assignments;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Test.Assignments
{
    public class DeleteAssignmentTests
    {  
        private Mock<IAssignmentRepository> _assignmentRepository;

        private DeleteAssignmentCommandValidator _deleteAssignmentValidator;

        private DeleteAssignmentHandler _insertAssignmentHandler;

        private DeleteAssignmentCommand _insertAssignmentCommand;

        [SetUp]
        public void Setup()
        {  
            _assignmentRepository = new();

            _assignmentRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(true);

            _assignmentRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(new DeleteResult.Acknowledged(1));

            _deleteAssignmentValidator = new();

            _insertAssignmentCommand = new DeleteAssignmentCommand
            {
                Id = It.IsAny<ObjectId>().ToString(),
                UserId = It.IsAny<ObjectId>().ToString(),
            };

            _insertAssignmentHandler = new(_assignmentRepository.Object , _deleteAssignmentValidator);
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
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(false);

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<AssignmentNotFoundError>();
        } 

        [Test]
        public async Task Should_Return_FailedToDeleteError()
        { 
            _assignmentRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(new DeleteResult.Acknowledged(0));

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<FailedToDeleteError>();
        }
    }
}
