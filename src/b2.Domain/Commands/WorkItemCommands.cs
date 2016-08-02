using System;

namespace b2.Domain.Commands
{
    public class CreateWorkItemFromTaskCommand
    {
        public CreateWorkItemFromTaskCommand(Guid workItemId, Guid taskId)
        {
            WorkItemId = workItemId;
            TaskId = taskId;
        }

        public Guid WorkItemId { get; }
        public Guid TaskId { get; }
    }

    public class CreateWorkItemFromBranchCommand
    {
        public CreateWorkItemFromBranchCommand(Guid id, Guid branchId)
        {
            Id = id;
            BranchId = branchId;
        }

        public Guid Id { get; }
        public Guid BranchId { get; }
    }

    public class AssignTaskToWorkItemCommand
    {
        public AssignTaskToWorkItemCommand(Guid id, Guid taskId)
        {
            Id = id;
            TaskId = taskId;
        }

        public Guid Id { get; }
        public Guid TaskId { get; }
    }
}