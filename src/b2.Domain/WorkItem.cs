using System;
using b2.Domain.Core;
using b2.Domain.Events;

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
            HandleEvent(new WorkItemCreatedFromBranch(id, branch.Id), true);
        }

        public WorkItem()
        {
            Status = "new";
        }

        public string Status { get; }
        public string TaskId { get; private set; }
        public string BranchId { get; private set; }

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
            if (BranchId != null && BranchId != branch.Id)
            {
                throw new InvalidOperationException("Cannot change branch");
            }

            HandleEvent(new BranchAssignedToWorkItem(Id, branch.Id), true);
        }

        public void Handle(WorkItemCreatedFromTask @event)
        {
            Id = @event.Id;
            TaskId = @event.TaskId;
        }

        public void Handle(WorkItemCreatedFromBranch @event)
        {
            Id = @event.Id;
            BranchId = @event.BranchId;
        }

        public void Handle(TaskAssignedToWorkItem @event)
        {
            TaskId = @event.TaskId;
        }

        public void Handle(BranchAssignedToWorkItem @event)
        {
            BranchId = @event.BranchId;
        }
    }
}