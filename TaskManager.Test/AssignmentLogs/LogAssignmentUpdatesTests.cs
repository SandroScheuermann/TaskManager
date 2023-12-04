using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.Events.AssignmentLogs;
using TaskManager.Application.Handlers.AssignmentLogs;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Test.AssignmentLogs
{
    public class LogAssignmentUpdatesTests
    {
        private Mock<IAssignmentLogRepository> _assignmentLogRepository;

        private Mock<IAssignmentRepository> _assignmentRepository;

        private LogAssignmentUpdatesEventHandler _logAssignmentUpdatesHandler;

        private LogAssignmentUpdatesEvent _assignmentLogEvent;

        [SetUp]
        public void Setup()
        {
            _assignmentLogRepository = new();

            _assignmentRepository = new();

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

            _assignmentLogEvent = new()
            {
                AssignmentId = It.IsAny<ObjectId>().ToString(),
                UserId = It.IsAny<ObjectId>().ToString(),
                OperationType = OperationTypeEnum.Comment,
            };

            _logAssignmentUpdatesHandler = new(_assignmentLogRepository.Object, _assignmentRepository.Object);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _logAssignmentUpdatesHandler.Handle(_assignmentLogEvent, default);

            response.Error.Should().BeNull();
        }
    }
}
