using b2.Domain.Core;

namespace b2.Domain
{
    public class Task : AggregateRoot
    {
        public Task()
        {
        }

        public Task(string id, string name, string url, string status)
        {
            HandleEvent(new TaskCreated(id, name, url, status), true);
        }

        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Status { get; private set; }

        public void ChangeStatus(string status)
        {
            HandleEvent(new TaskStatusChanged(Id, status), true);
        }

        public void Handle(TaskCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Url = @event.Url;
            Status = @event.Status;
        }

        public void Handle(TaskStatusChanged @event)
        {
            Status = @event.Status;
        }
    }

    public class TaskCreated : Event
    {
        public TaskCreated(string id, string name, string url, string status) : base(id)
        {
            Name = name;
            Url = url;
            Status = status;
        }

        public string Name { get; }
        public string Url { get; }
        public string Status { get; }
    }

    public class TaskStatusChanged : Event
    {
        public TaskStatusChanged(string id, string status) : base(id)
        {
            Status = status;
        }

        public string Status { get; }
    }
}