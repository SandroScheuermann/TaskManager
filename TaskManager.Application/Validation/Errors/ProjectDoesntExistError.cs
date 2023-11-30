using TaskManager.Application.Validation.ErrorHandling;

namespace TaskManager.Application.Validation.Errors
{
    public class ProjectDoesntExistError : Error
    {
        public ProjectDoesntExistError() : base("The informed project doesn't exist in the database.")
        {
        }
    }
}
