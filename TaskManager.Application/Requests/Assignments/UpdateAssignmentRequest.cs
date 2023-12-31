﻿using TaskManager.Domain.Enums;

namespace TaskManager.Application.Requests.Assignments
{
    public class UpdateAssignmentRequest
    {
        public required string UserId { get; set; }

        public string? Description { get; set; }

        public AssignmentStatusEnum? Status { get; set; }

    }
}