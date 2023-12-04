using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;
using TaskManger.Infra.Repositories.Shared;

namespace TaskManger.Infra.Repositories.Assignments
{
    public class AssignmentLogRepository(IOptions<DefaultSettings> settings) : MongoRepository<AssignmentLog>(settings), IAssignmentLogRepository
    {
        public async Task<IEnumerable<AssignmentLog>> GetCompletedAssignmentsByUser(string userId, int numberOfDaysAgo = 0)
        {
            var daysAgo = DateTime.Now.AddDays(-numberOfDaysAgo);

            var filter = Builders<AssignmentLog>.Filter.Eq(a => a.UserId, userId)
                        & Builders<AssignmentLog>.Filter.Eq(a => a.OperationType, OperationTypeEnum.Update)
                        & Builders<AssignmentLog>.Filter.Eq(a => a.AssignmentState.Status, AssignmentStatusEnum.Done)
                        & Builders<AssignmentLog>.Filter.Gte(a => a.OperationDate, daysAgo);

            var cursor = await Collection.FindAsync(filter);
            var assignments = await cursor.ToListAsync();

            return assignments;
        } 
    }
}
