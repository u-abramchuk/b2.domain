using System;
using b2.Domain.Core;

namespace b2.Domain
{
    public class WorkItem : AggregateRoot
    {
        public WorkItem(string id, Task task) : this()
        {
            HandleEvent(new WorkItemCreatedFromTask(id, task.Id), true);
        }

        public WorkItem(string id, Branch branch) : this()
        {
            HandleEvent(new WorkItemCreatedFromBranch(id, branch), true);
        }

        public WorkItem()
        {
            Status = "new";
        }

        public string Status { get; }
        public string TaskId { get; private set; }
        public Branch Branch { get; private set; }

        public void AssignTask(Task task)
        {
            if (TaskId != null && TaskId != task.Id)
            {
                throw new InvalidOperationException("Cannot change task");
            }

            HandleEvent(new TaskAssignedToWorkItem(Id, task.Id), true);
        }

        public void AssignBranch(Branch branch)
        {
            if (Branch != null && Branch != branch)
            {
                throw new InvalidOperationException("Cannot change branch");
            }

            HandleEvent(new BranchAssignedToWorkItem(Id, branch), true);
        }

        public void Handle(WorkItemCreatedFromTask @event)
        {
            Id = @event.Id;
            TaskId = @event.TaskId;
        }

        public void Handle(WorkItemCreatedFromBranch @event)
        {
            Id = @event.Id;
            Branch = @event.Branch;
        }

        public void Handle(TaskAssignedToWorkItem @event)
        {
            TaskId = @event.TaskId;
        }

        public void Handle(BranchAssignedToWorkItem @event)
        {
            Branch = @event.Branch;
        }
    }
    public class Branch : IEntity
    {
        public Branch(string id)
        {
            Id = id;
        }
        public string Id { get; }
    }

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
        public WorkItemCreatedFromBranch(string id, Branch branch) : base(id)
        {
            Branch = branch;
        }

        public Branch Branch { get; }
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
        public BranchAssignedToWorkItem(string id, Branch branch) : base(id)
        {
            Branch = branch;
        }

        public Branch Branch { get; }
    }
}