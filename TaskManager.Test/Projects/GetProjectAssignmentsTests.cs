using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Handlers.Projects;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Assignments;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;
using TaskManager.Domain.Repositories.Projects;

namespace TaskManager.Test.Projects
{
    public class GetProjectAssignmentsTests
    {
        private Mock<IProjectRepository> _projectRepository;

        private Mock<IAssignmentRepository> _assignmentRepository;

        private GetProjectAssignmentsHandler _getProjectAssignmentsHandler;

        private GetProjectAssignmentsCommand _getProjectAssignmentsCommand;

        private GetProjectAssignmentsCommandValidator _getProjectAssignmentsCommandValidator;

        [SetUp]
        public void Setup()
        {
            _assignmentRepository = new();

            _assignmentRepository
                .Setup(repo => repo.GetAssignmentsByProjectId(It.IsAny<string>()))
                .ReturnsAsync(new List<Assignment> {
                    new Assignment
                    {
                        Id = It.IsAny<ObjectId>().ToString(),
                        ProjectId = It.IsAny<ObjectId>().ToString(),
                        Description = It.IsAny<string>(),
                        ExpirationDate = It.IsAny<DateTime>(),
                        Priority = It.IsAny<AssignmentPriorityEnum>(),
                        Title = It.IsAny<string>(),
                        Status = It.IsAny<AssignmentStatusEnum>(),
                    }
                });

            _projectRepository = new();

            _projectRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(true);

            _getProjectAssignmentsCommand = new() { ProjectId = It.IsAny<ObjectId>().ToString() };

            _getProjectAssignmentsCommandValidator = new();

            _getProjectAssignmentsHandler = new(_assignmentRepository.Object, _projectRepository.Object, _getProjectAssignmentsCommandValidator);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _getProjectAssignmentsHandler.Handle(_getProjectAssignmentsCommand, default);

            response.Error.Should().BeNull();
        }

        [Test]
        public async Task Should_Return_RequestValidationError()
        {
            _getProjectAssignmentsCommand.ProjectId = "notAnObjectId";

            var response = await _getProjectAssignmentsHandler.Handle(_getProjectAssignmentsCommand, default);

            response.Error.Should().BeOfType<RequestValidationError>();
        }

        [Test]
        public async Task Should_Return_ProjectNotFoundError()
        {
            _projectRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(false);

            var response = await _getProjectAssignmentsHandler.Handle(_getProjectAssignmentsCommand, default);

            response.Error.Should().BeOfType<ProjectNotFoundError>();
        }
    }
}
