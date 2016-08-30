using System;
using b2.Domain.Core;

namespace b2.Domain.Events
{
    public class WorkspaceCreated : Event
    {
        public WorkspaceCreated(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; }
    }
}