﻿using MongoDB.Bson;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Requests.Assignments
{
    public class InsertAssignmentRequest
    {
        public required string ProjectId { get; set; }

        public required string UserId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required DateTime ExpirationDate { get; set; }

        public required AssignmentStatusEnum Status { get; set; }

        public required AssignmentPriorityEnum Priority{ get; set; }
    }
} 