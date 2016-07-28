using System;
using b2.Domain.Core;

namespace b2.Domain
{
    public class WorkItem : AggregateRoot
    {
        public WorkItem(string id, Task task) : this()
        {
            HandleEvent(new WorkItemCreatedFromTask(id, task), true);
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
        public Task Task { get; private set; }
        public Branch Branch { get; private set; }

        public void AssignTask(Task task)
        {
            if (Task != null && Task != task)
            {
                throw new InvalidOperationException("Cannot change task");
            }

            HandleEvent(new TaskAssignedToWorkItem(Id, task), true);
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
            Task = @event.Task;
        }

        public void Handle(WorkItemCreatedFromBranch @event)
        {
            Id = @event.Id;
            Branch = @event.Branch;
        }

        public void Handle(TaskAssignedToWorkItem @event)
        {
            Task = @event.Task;
        }

        public void Handle(BranchAssignedToWorkItem @event)
        {
            Branch = @event.Branch;
        }

        public void SetTaskState(string status)
        {
            if (Task == null)
            {
                throw new InvalidOperationException(
                    "Cannot change task state as task is not set");
            }

            Task.SetStatus(status);
        }
    }

    public class Task : IDomainEntity
    {
        public Task(string id, string name, string url, string status)
        {
            Id = id;
            Name = name;
            Url = url;
            Status = status;
        }

        public string Id { get; }
        public string Name { get; }
        public string Url { get; }
        public string Status { get; private set; }

        public void SetStatus(string status)
        {
            Status = status;
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
        public WorkItemCreatedFromTask(string id, Task task) : base(id)
        {
            Task = task;
        }

        public Task Task { get; }
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
        public TaskAssignedToWorkItem(string id, Task task) : base(id)
        {
            Task = task;
        }

        public Task Task { get; }
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