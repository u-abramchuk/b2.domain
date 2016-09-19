using System;
using b2.Domain.Core;

namespace b2.Domain.Events
{
    public class WorkspaceCreated : Event
    {
        public WorkspaceCreated(Guid id, string name, string creator)
        {
            Id = id;
            Name = name;
            Creator = creator;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Creator { get; }
    }
}