using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Handlers.Projects;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Projects;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;
using TaskManager.Domain.Repositories.Projects;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Test.Projects
{
    public class DeleteProjectTests
    {
        private Mock<IProjectRepository> _projectRepository;

        private Mock<IAssignmentRepository> _assignmentRepository;

        private DeleteProjectCommandValidator _insertProjectValidator;

        private DeleteProjectHandler _insertAssignmentHandler;

        private DeleteProjectCommand _insertAssignmentCommand;

        [SetUp]
        public void Setup()
        {
            _assignmentRepository = new();

            _assignmentRepository
                .Setup(repo => repo.GetPendingAssignmentsByProjectId(It.IsAny<string>()))
                .ReturnsAsync(new List<Assignment>()); 

            _projectRepository = new();

            _projectRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(true);

            _projectRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(new DeleteResult.Acknowledged(1));

            _insertProjectValidator = new();

            _insertAssignmentCommand = new DeleteProjectCommand
            {
                Id = It.IsAny<ObjectId>().ToString(),
            };

            _insertAssignmentHandler = new(_projectRepository.Object, _assignmentRepository.Object, _insertProjectValidator);
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
        public async Task Should_Return_ProjectNotFoundError()
        {
            _projectRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(false);

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<ProjectNotFoundError>();
        }

        [Test]
        public async Task Should_Return_PendingAssignmentsError()
        {
            _assignmentRepository
                .Setup(repo => repo.GetPendingAssignmentsByProjectId(It.IsAny<string>()))
                .ReturnsAsync(new List<Assignment> { It.Is<Assignment>(x => x.Status == AssignmentStatusEnum.Pending) });

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<PendingAssignmentsError>();
        }

        [Test]
        public async Task Should_Return_FailedToDeleteError()
        { 
            _projectRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(new DeleteResult.Acknowledged(0));

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<FailedToDeleteError>();
        }
    }
}
