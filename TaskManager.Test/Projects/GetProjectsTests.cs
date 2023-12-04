using FluentAssertions;
using MongoDB.Driver;
using Moq;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Handlers.Projects;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Entities.Projects;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Projects;

namespace TaskManager.Test.Projects
{
    public class GetProjectsTests
    {
        private Mock<IProjectRepository> _projectRepository;

        private GetProjectsHandler _insertAssignmentHandler;

        private GetProjectsCommand _insertAssignmentCommand;

        [SetUp]
        public void Setup()
        {
            _projectRepository = new();

            _projectRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(It.IsAny<IEnumerable<Project>>());

            _insertAssignmentCommand = new();

            _insertAssignmentHandler = new(_projectRepository.Object);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeNull();
        } 
    }
}
