using System;
using b2.Domain.Core;
using b2.Domain.Events;

namespace b2.Domain.Entities
{
    public class Workspace : AggregateRoot
    {
        public Workspace(Guid id, string name, string creatorId)
        {
            HandleEvent(new WorkspaceCreated(id, name, creatorId), true);
        }

        public Workspace()
        {
        }

        public string Name { get; private set; }
        public string CreatorId { get; private set; }

        public void Handle(WorkspaceCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            CreatorId = @event.CreatorId;
        }
    }
}