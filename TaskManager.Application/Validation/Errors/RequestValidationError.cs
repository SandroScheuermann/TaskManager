using FluentValidation.Results;
using TaskManager.Application.Validation.ResultHandling;

namespace TaskManager.Application.Validation.Errors
{
    public class RequestValidationError(IEnumerable<ValidationFailure> failures) : Error($"The request has invalid parameters : {string.Join(',', failures)}")
    {
    }
}
