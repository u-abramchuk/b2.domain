using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace b2.Domain.Core
{
    public interface IEventStore
    {
        Task SaveEvents(Guid aggregateId, IEnumerable<EventDescriptor> events);
        Task<IReadOnlyCollection<EventDescriptor>> GetAll(Guid aggregateId);
    }
}