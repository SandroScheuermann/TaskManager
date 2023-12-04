using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Handlers.Users;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Users;
using TaskManager.Domain.Entities.Projects;
using TaskManager.Domain.Repositories.Projects;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Test.Users
{
    public class GetUserProjectsTests
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IProjectRepository> _projectRepository;

        private GetUserProjectsCommandValidator _getUserProjectsValidator;

        private GetUserProjectsHandler _getUserProjectsHandler;

        private GetUserProjectsCommand _getUserProjectsCommand;

        [SetUp]
        public void Setup()
        {
            _userRepository = new();

            _userRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(true);

            _projectRepository = new();

            _projectRepository
                .Setup(repo => repo.GetProjectsByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<IEnumerable<Project>>);

            _getUserProjectsValidator = new();

            _getUserProjectsCommand = new GetUserProjectsCommand
            {
                UserId = It.IsAny<ObjectId>().ToString(),
            };

            _getUserProjectsHandler = new(_projectRepository.Object, _userRepository.Object, _getUserProjectsValidator);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _getUserProjectsHandler.Handle(_getUserProjectsCommand, default);

            response.Error.Should().BeNull();
        }


        [Test]
        public async Task Should_Return_RequestValidationError()
        {
            _getUserProjectsCommand.UserId = "notAnObjectId";

            var response = await _getUserProjectsHandler.Handle(_getUserProjectsCommand, default);

            response.Error.Should().BeOfType<RequestValidationError>();
        }

        [Test]
        public async Task Should_Return_UserNotFoundError()
        {
            _userRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(false);

            var response = await _getUserProjectsHandler.Handle(_getUserProjectsCommand, default);

            response.Error.Should().BeOfType<UserNotFoundError>();
        }
    }
}
