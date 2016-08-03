using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace b2.Domain.Core
{
    public interface IEventStorage
    {
        Task SaveEvents(Guid aggregateId, IEnumerable<Event> events);
        Task<IReadOnlyCollection<Event>> GetAll(Guid aggregateId);
    }
}