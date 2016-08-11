using System.Collections.Generic;

namespace b2.Domain.Core
{
    public interface IEventPublisher
    {
        void Publish(IEnumerable<EventDescriptor> events);
    }
}