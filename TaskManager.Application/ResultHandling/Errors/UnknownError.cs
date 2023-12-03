namespace TaskManager.Application.ResultHandling.Errors
{
    public class UnknownError(string? message = null) : Error(message ?? "Erro desconhecido")
    {
    }
}
