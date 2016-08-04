using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using b2.Domain.Core;

namespace b2.Domain.Tests
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly List<Event> _storage = new List<Event>();

        public Task SaveEvents(Guid aggregateId, IEnumerable<Event> events)
        {
            return Task.Run(() => SaveEventsSync(aggregateId, events));
        }
       
        public Task<IReadOnlyCollection<Event>> GetAll(Guid aggregateId)
        {
            return Task.FromResult(GetAllSync(aggregateId));
        }

        public void SaveEventsSync(Guid aggregateId, IEnumerable<Event> events)
        {
            _storage.AddRange(events);
        }

         public IReadOnlyCollection<Event> GetAllSync(Guid aggregateId)
        {
            return _storage
                .Where(x => x.Id == aggregateId)
                .ToList()
                .AsReadOnly();
        }

    }
}