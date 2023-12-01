using System.Diagnostics;

namespace TaskManager.Application.Validation.ResultHandling
{
    public class Result<TSuccess, TFailure>
    {
        private Result(bool isSucces, TFailure error)
        {
            IsSuccess = isSucces;
            Error = error;
        }
        private Result(bool isSucces, TSuccess value)
        {
            IsSuccess = isSucces;
            Value = value;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public TSuccess? Value { get; set; }

        public TFailure? Error { get; set; }

        public static implicit operator Result<TSuccess, TFailure>(TSuccess value) => new(true, value);

        public static implicit operator Result<TSuccess, TFailure>(TFailure error) => new(false, error);

        public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TFailure, TResult> onFailure)
        {
            if (Value is not null)
            {
                return onSuccess(Value);
            }
            if (Error is not null)
            {
                return onFailure(Error);
            }

            throw new UnreachableException();
        }

        public Result<TNewSuccess, TFailure> Bind<TNewSuccess>(Func<TSuccess, Result<TNewSuccess, TFailure>> func)
        {
            if (Value is not null)
            {
                return func(Value);
            }
            else if (Error is not null)
            {
                return Error;
            }

            throw new UnreachableException();
        }

        public async Task<Result<TNewSuccess, TFailure>> BindAsync<TNewSuccess>(Task<Func<TSuccess, Task<Result<TNewSuccess, TFailure>>>> func)
        {
            if (Value is not null)
            {
                return await func.Result(Value);
            }
            else if (Error is not null)
            {
                return Error; 
            }

            throw new UnreachableException();
        }    

    }
    public abstract class Error(string message)
    {
        public string? Message { get; set; } = message;
    }
}
