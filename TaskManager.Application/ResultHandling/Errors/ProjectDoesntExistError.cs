namespace TaskManager.Application.ResultHandling.Errors
{
    public class ProjectDoesntExistError() : Error("O projeto informado não existe no banco de dados.")
    {
    }
}
