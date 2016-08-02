using System;

namespace b2.Domain.Commands
{
    public class CreateTaskCommand
    {
        public CreateTaskCommand(Guid id, string name, string url, string status)
        {
            Id = id;
            Name = name;
            Url = url;
            Status = status;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Url { get; }
        public string Status { get; }
    }
}