using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using b2.Domain.Core;

namespace b2.Domain.Tests
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly List<EventDescriptor> _storage = new List<EventDescriptor>();

        public Task SaveEvents(Guid aggregateId, IEnumerable<EventDescriptor> events)
        {
            return Task.Run(() => SaveEventsSync(aggregateId, events));
        }
       
        public Task<IReadOnlyCollection<EventDescriptor>> GetAll(Guid aggregateId)
        {
            return Task.FromResult(GetAllSync(aggregateId));
        }

        public void SaveEventsSync(Guid aggregateId, IEnumerable<EventDescriptor> events)
        {
            _storage.AddRange(events);
        }

         public IReadOnlyCollection<EventDescriptor> GetAllSync(Guid aggregateId)
        {
            return _storage
                .Where(x => x.Event.Id == aggregateId)
                .ToList()
                .AsReadOnly();
        }

    }
}