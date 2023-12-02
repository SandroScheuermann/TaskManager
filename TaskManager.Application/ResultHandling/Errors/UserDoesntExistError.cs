namespace TaskManager.Application.ResultHandling.Errors
{
    public class UserDoesntExistError() : Error("The informed user doesn't exist in the database.")
    { 
    }
}
