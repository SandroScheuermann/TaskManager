namespace TaskManager.Domain.Entities.ConfigurationModels
{
    public class DefaultSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
