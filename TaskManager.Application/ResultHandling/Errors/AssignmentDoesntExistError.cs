namespace TaskManager.Application.ResultHandling.Errors
{
    public class ProjectDoesntExistError : Error
    {
        public ProjectDoesntExistError() : base("The informed project doesn't exist in the database.")
        {
        }
    }
}
