using b2.Domain.Core;

namespace b2.Domain.Events
{
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