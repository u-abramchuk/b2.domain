namespace b2.Domain
{
    public class CreateWorkItemFromTaskCommand
    {
        public CreateWorkItemFromTaskCommand(string workItemId, string taskId)
        {
            WorkItemId = workItemId;
            TaskId = taskId;
        }

        public string WorkItemId { get; }
        public string TaskId { get; }
    }

    public class CreateWorkItemFromBranchCommand
    {
        public CreateWorkItemFromBranchCommand(string id, Branch branch)
        {
            Id = id;
            Branch = branch;
        }

        public string Id { get; }
        public Branch Branch { get; }
    }

    public class AssignTaskToWorkItemCommand
    {
        public AssignTaskToWorkItemCommand(string id, string taskId)
        {
            Id = id;
            TaskId = taskId;
        }

        public string Id { get; }
        public string TaskId { get; }
    }
}