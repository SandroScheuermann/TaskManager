using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Requests.Users;
using TaskManager.Application.Responses.Users;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Users
{
    public class InsertUserHandler(IUserRepository userRepository, IValidator<InsertUserRequest> projectValidator)
        : IRequestHandler<InsertUserCommand, Result<InsertUserResponse, Error>>
    { 
        public IUserRepository UserRepository { get; set; } = userRepository; 
        public IValidator<InsertUserRequest> UserValidator { get; set; } = projectValidator;

        public Task<Result<InsertUserResponse, Error>> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command.Request) 
               .Bind(CreateAndInsertUser);

            return Task.FromResult(response);
        }

        private Result<InsertUserRequest, Error> ValidateRequest(InsertUserRequest request)
        {
            var validationResult = UserValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }  
        private Result<InsertUserResponse, Error> CreateAndInsertUser(InsertUserRequest request)
        {
            var user = new User()
            {
                Id = string.Empty, 
                UserName = request.UserName,
                Role = request.UserRole,
            };

            UserRepository.InsertAsync(user);

            var response = new InsertUserResponse()
            {
                Id = user.Id,
                UserName = request.UserName,
            };

            return response;
        }
    }
}
