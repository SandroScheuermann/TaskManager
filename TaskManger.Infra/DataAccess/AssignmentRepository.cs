using Microsoft.Extensions.Options;
using Muscler.Infra.DataAccess.Shared;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManger.Infra.DataAccess
{
    public class AssignmentRepository(IOptions<DefaultSettings> settings) : MongoRepository<Assignment>(settings), IAssignmentRepository
    {
    }
}
