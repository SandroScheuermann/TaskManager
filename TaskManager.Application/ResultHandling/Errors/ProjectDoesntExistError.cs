namespace TaskManager.Application.ResultHandling.Errors
{
    public class ProjectDoesntExistError() : Error("The informed project doesn't exist in the database.")
    {
    }
}
