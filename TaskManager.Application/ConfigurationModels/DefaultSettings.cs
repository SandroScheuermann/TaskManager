using System.Diagnostics.CodeAnalysis;

namespace TaskManager.Application.ConfigurationModels
{
    [ExcludeFromCodeCoverage]
    public class DefaultSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
