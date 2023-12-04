using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Handlers.Projects;
using TaskManager.Application.Requests.Projects;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Projects;
using TaskManager.Domain.Repositories.Projects;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Test.Projects
{
    public class InsertProjectTests
    {
        private Mock<IProjectRepository> _projectRepository;

        private Mock<IUserRepository> _userRepository; 

        private InsertProjectCommandValidator _insertProjectValidator;

        private InsertProjectHandler _insertAssignmentHandler;

        private InsertProjectCommand _insertAssignmentCommand; 

        [SetUp]
        public void Setup()
        {
            _userRepository = new(); 

            _userRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(true); 

            _projectRepository = new(); 

            _insertProjectValidator = new(); 

            _insertAssignmentCommand = new InsertProjectCommand
            { 
                Request = new InsertProjectRequest
                {
                    ProjectName = "ProjecNameTest",
                    UserId = It.IsAny<ObjectId>().ToString(),
                }
            };

            _insertAssignmentHandler = new(_projectRepository.Object, _userRepository.Object, _insertProjectValidator);
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
            _insertAssignmentCommand.Request.ProjectName = null;

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<RequestValidationError>();
        }

        [Test]
        public async Task Should_Return_UserNotFoundError()
        { 
            _userRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(false);

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<UserNotFoundError>();
        }

    }
}
