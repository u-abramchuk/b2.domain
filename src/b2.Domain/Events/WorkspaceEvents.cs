using System;
using b2.Domain.Core;

namespace b2.Domain.Events
{
    public class WorkspaceCreated : Event
    {
        public WorkspaceCreated(Guid id, string name, string creatorId) : base(id)
        {
            Name = name;
            CreatorId = creatorId;
        }

        public string Name { get; }
        public string CreatorId { get; }
    }
}