﻿using TaskManager.Domain.Entities.Shared;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class User : MongoEntity
    {
        public required string? UserName { get; set; }

        public required UserRoleEnum Role { get; set; }
    }
}
