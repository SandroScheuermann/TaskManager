namespace TaskManager.Application.ResultHandling.Errors
{
    public class AssignmentDoesntExistError : Error
    {
        public AssignmentDoesntExistError() : base("The informed assignment doesn't exist in the database.")
        {
        }
    }
}
