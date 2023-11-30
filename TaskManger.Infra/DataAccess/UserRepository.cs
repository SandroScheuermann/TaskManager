﻿using Microsoft.Extensions.Options;
using Muscler.Infra.DataAccess.Shared;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Entities.ConfigurationModels;
using TaskManager.Domain.Repositories;

namespace TaskManger.Infra.DataAccess
{
    public class UserRepository(IOptions<DefaultSettings> settings) : MongoRepository<User>(settings), IUserRepository
    {
    }
}
