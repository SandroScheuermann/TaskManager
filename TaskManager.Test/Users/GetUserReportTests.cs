using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Handlers.Users;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Users;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Entities.Users;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Test.Users
{
    public class GetUserReportTests
    {
        private Mock<IAssignmentLogRepository> _assignmentLogRepository;
        private Mock<IUserRepository> _userRepository;

        private GetUserReportCommandValidator _getUserReportValidator;

        private GetUserReportHandler _getUserReportHandler;

        private GetUserReportCommand _getUserReportCommand;

        [SetUp]
        public void Setup()
        {
            _userRepository = new();

            _userRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User 
                { 
                    Id = It.IsAny<ObjectId>().ToString(), 
                    Role = UserRoleEnum.Manager, 
                    UserName = It.IsAny<string>()
                });

            _assignmentLogRepository = new();

            _assignmentLogRepository
                .Setup(repo => repo.GetCompletedAssignmentsByUser(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<AssignmentLog>
                {
                    new AssignmentLog
                    {
                        Id = It.IsAny<string>(),
                        UserId = It.IsAny<string>(),
                        AssignmentId = It.IsAny<string>(),
                        AssignmentState = It.IsAny<Assignment>(),
                        OperationDate = It.IsAny<DateTime>(),
                        OperationType = It.IsAny<OperationTypeEnum>(),                    
                    } 
                });

            _getUserReportValidator = new();

            _getUserReportCommand = new GetUserReportCommand
            {
                UserId = It.IsAny<ObjectId>().ToString(),
                ManagerUserId = It.IsAny<ObjectId>().ToString()
            };

            _getUserReportHandler = new(_assignmentLogRepository.Object, _userRepository.Object, _getUserReportValidator);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _getUserReportHandler.Handle(_getUserReportCommand, default);

            response.Error.Should().BeNull();
        } 

        [Test]
        public async Task Should_Return_RequestValidationError()
        {
            _getUserReportCommand.UserId = "notAnObjectId";
            _getUserReportCommand.ManagerUserId = "notAnObjectId";

            var response = await _getUserReportHandler.Handle(_getUserReportCommand, default);

            response.Error.Should().BeOfType<RequestValidationError>();
        }

        [Test]
        public async Task Should_Return_UserNotFoundError()
        {
            _userRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(It.Is<User>(user => user.Id == null));

            var response = await _getUserReportHandler.Handle(_getUserReportCommand, default);

            response.Error.Should().BeOfType<UserNotFoundError>();
        }

        [Test]
        public async Task Should_Return_UserNotAuthorizedError()
        {
            _userRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User
                {
                    Id = It.IsAny<ObjectId>().ToString(),
                    Role = UserRoleEnum.Employee,
                    UserName = It.IsAny<string>()
                });

            var response = await _getUserReportHandler.Handle(_getUserReportCommand, default);

            response.Error.Should().BeOfType<UserUnathorizedError>();
        }
    }
}
