using FluentValidation.Results;

namespace TaskManager.Application.ResultHandling.Errors
{
    public class RequestValidationError(IEnumerable<ValidationFailure> failures) : Error($"A requisição possui parâmetros inválidos: {string.Join(',', failures)}")
    {
    }
}
