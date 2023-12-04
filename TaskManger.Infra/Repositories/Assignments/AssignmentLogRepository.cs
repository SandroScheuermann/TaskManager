using Microsoft.Extensions.Options;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Repositories.Assignments;
using TaskManger.Infra.Repositories.Shared;

namespace TaskManger.Infra.Repositories.Assignments
{
    public class AssignmentLogRepository(IOptions<DefaultSettings> settings) : MongoRepository<AssignmentLog>(settings), IAssignmentLogRepository
    { 
    }
}
