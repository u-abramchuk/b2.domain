using System;

namespace b2.Domain.Core
{
    public class EventDescriptor
    {
        public EventDescriptor(Guid id, string eventType, int version, Event @event)
        {
            Id = id;
            EventType = eventType;
            Version = version;
            Event = @event;
        }

        public Guid Id { get; }
        public string EventType { get; }
        public int Version { get; }
        public Event Event { get; }
    }
}