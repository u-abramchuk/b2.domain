using b2.Domain.Core;
using b2.Domain.Events;

namespace b2.Domain.Entities
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
}