using FluentAssertions;
using Moq;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Handlers.Assignments;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Entities.Projects;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Test.Assignments
{
    public class GetAssignmentsTests
    {
        private Mock<IAssignmentRepository> _assignmentRepository;

        private GetAssignmentsHandler _insertAssignmentHandler;

        private GetAssignmentsCommand _insertAssignmentCommand;

        [SetUp]
        public void Setup()
        {
            _assignmentRepository = new();

            _assignmentRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(It.IsAny<IEnumerable<Assignment>>());

            _insertAssignmentCommand = new();

            _insertAssignmentHandler = new(_assignmentRepository.Object);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeNull();
        } 
    }
}
