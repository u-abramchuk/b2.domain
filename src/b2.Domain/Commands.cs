namespace b2.Domain
{
    public class CreateWorkItemFromTaskCommand
    {
        public CreateWorkItemFromTaskCommand(string id, Task task)
        {
            Id = id;
            Task = task;
        }

        public string Id { get; }
        public Task Task { get; }
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
        public AssignTaskToWorkItemCommand(string id, Task task)
        {
            Id = id;
            Task = task;
        }

        public string Id { get; }
        public Task Task { get; }
    }
}