using FluentValidation;
using System.Text.RegularExpressions;

namespace TaskManager.Application.Validators.Shared
{
    public static partial class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValidObjectId<T>(
            this IRuleBuilder<T, string> ruleBuilder, string message = "")
        {
            return ruleBuilder.Must(BeAValidObjectId).WithMessage(message);
        }

        private static bool BeAValidObjectId(string projectId)
        { 
            return ObjectIdRegex().IsMatch(projectId);
        }

        [GeneratedRegex("^[0-9a-fA-F]{24}$")]
        private static partial Regex ObjectIdRegex();
    }
}
