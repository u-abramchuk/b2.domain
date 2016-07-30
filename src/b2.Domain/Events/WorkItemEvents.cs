using b2.Domain.Core;

namespace b2.Domain.Events
{
     public class WorkItemCreatedFromTask : Event
    {
        public WorkItemCreatedFromTask(string id, string taskId) : base(id)
        {
            TaskId = taskId;
        }

        public string TaskId { get; }
    }

    public class WorkItemCreatedFromBranch : Event
    {
        public WorkItemCreatedFromBranch(string id, string branchId) : base(id)
        {
            BranchId = branchId;
        }

        public string BranchId { get; }
    }

    public class TaskAssignedToWorkItem : Event
    {
        public TaskAssignedToWorkItem(string id, string taskId) : base(id)
        {
            TaskId = taskId;
        }

        public string TaskId { get; }
    }

    public class BranchAssignedToWorkItem : Event
    {
        public BranchAssignedToWorkItem(string id, string branchId) : base(id)
        {
            BranchId = branchId;
        }

        public string BranchId { get; }
    }
}