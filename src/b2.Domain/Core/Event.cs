using System;

namespace b2.Domain.Core
{
    public abstract class Event
    {
        public Event(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}