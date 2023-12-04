using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Handlers.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Assignments;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;
using TaskManager.Domain.Repositories.Projects;

namespace TaskManager.Test.Assignments
{
    public class InsertAssignmentTests
    {
        private Mock<IAssignmentRepository> _assignmentRepository;

        private Mock<IProjectRepository> _projectRepository;

        private InsertAssignmentCommandValidator _insertAssignmentValidator;

        private InsertAssignmentHandler _insertAssignmentHandler;

        private InsertAssignmentCommand _insertAssignmentCommand;

        private InsertAssignmentRequest _insertAssignmentRequest;

        [SetUp]
        public void Setup()
        {
            _assignmentRepository = new();

            _assignmentRepository
                .Setup(repo => repo.GetAssignmentsCountByProjectId(It.IsAny<string>()))
                .ReturnsAsync(1);

            _projectRepository = new();

            _projectRepository
                .Setup(repo => repo.CheckExistanceById(It.IsAny<string>()))
                .ReturnsAsync(true);

            _insertAssignmentValidator = new();

            _insertAssignmentCommand = new InsertAssignmentCommand
            {
                Request = new InsertAssignmentRequest
                {
                    UserId = It.IsAny<ObjectId>().ToString(),
                    ProjectId = It.IsAny<ObjectId>().ToString(),
                    Description = "descricaoTeste",
                    Title = "tituloTeste",
                    ExpirationDate = DateTime.Now.AddDays(1),
                    Priority = It.IsAny<AssignmentPriorityEnum>(),
                    Status = It.IsAny<AssignmentStatusEnum>(),
                }
            };

            _insertAssignmentHandler = new(_assignmentRepository.Object, _projectRepository.Object, _insertAssignmentValidator);
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
            _insertAssignmentCommand.Request.Title = null;

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
        public async Task Should_Return_MaximumNumberOfAssignmentsError()
        {
            _assignmentRepository
                .Setup(repo => repo.GetAssignmentsCountByProjectId(It.IsAny<string>()))
                .ReturnsAsync(20);

            var response = await _insertAssignmentHandler.Handle(_insertAssignmentCommand, default);

            response.Error.Should().BeOfType<MaximumNumberOfAssignmentsError>();
        }
    }
}
