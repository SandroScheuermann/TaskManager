using Microsoft.Extensions.Options;
using Muscler.Infra.DataAccess.Shared;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Entities.ConfigurationModels;
using TaskManager.Domain.Repositories;

namespace TaskManger.Infra.DataAccess
{
    public class ProjectRepository(IOptions<DefaultSettings> settings) : MongoRepository<Project>(settings), IProjectRepository
    {
    }
}
