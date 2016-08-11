using System.Collections.Generic;
using b2.Domain.Core;

namespace b2.Domain.Tests
{
    public class InMemoryEventPublisher : IEventPublisher
    {

        public void Publish(IEnumerable<EventDescriptor> events)
        {
            PublishedEvents.AddRange(events);
        }

        public List<EventDescriptor> PublishedEvents { get; } = new List<EventDescriptor>();
    }
}