using FluentValidation.Results;

namespace TaskManager.Application.ResultHandling.Errors
{
    public class RequestValidationError(IEnumerable<ValidationFailure> failures) : Error($"The request has invalid parameters : {string.Join(',', failures)}")
    {
    }
}
