using FluentAssertions;
using Moq;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Handlers.Users;
using TaskManager.Application.Requests.Users;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Application.Validators.Projects;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Test.Users
{
    public class InsertUserTests
    {
        private Mock<IUserRepository> _userRepository;

        private InsertUserCommandValidator _insertUserValidator;

        private InsertUserHandler _insertUserHandler;

        private InsertUserCommand _insertUserCommand;

        private InsertUserRequest _insertUserRequest;

        [SetUp]
        public void Setup()
        {
            _userRepository = new();
             
            _insertUserValidator = new();

            _insertUserRequest = new InsertUserRequest 
            {
                UserName = "UserNameTest",
                UserRole = UserRoleEnum.Manager
            };

            _insertUserCommand = new InsertUserCommand 
            { 
                Request = _insertUserRequest,
            };

            _insertUserHandler = new(_userRepository.Object, _insertUserValidator);
        }

        [Test]
        public async Task Should_Be_Success()
        {
            var response = await _insertUserHandler.Handle(_insertUserCommand, default); 

            response.Error.Should().BeNull();
        }


        [Test]
        public async Task Should_Return_RequestValidationError()
        {
            _insertUserCommand.Request.UserName = null;

            var response = await _insertUserHandler.Handle(_insertUserCommand, default);  

            response.Error.Should().BeOfType<RequestValidationError>(); 
        }

    }
}
