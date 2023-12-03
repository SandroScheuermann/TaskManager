namespace TaskManager.Application.ResultHandling.Errors
{
    public class AssignmentDoesntExistError() : Error("A tarefa informada não existe no banco de dados.")
    { 
    }
}
