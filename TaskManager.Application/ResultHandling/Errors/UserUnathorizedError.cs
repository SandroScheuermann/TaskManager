namespace TaskManager.Application.ResultHandling.Errors
{
    public class UserUnathorizedError(string userId) : Error($"O usuário {userId} não possui cargo de gerente.")
    {
    }
}
