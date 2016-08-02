using System;
using System.Collections.Generic;
using System.Linq;

namespace b2.Domain.Core
{
    public interface IEventStorage
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events);
        IReadOnlyCollection<Event> GetAll(Guid aggregateId);
    }
}