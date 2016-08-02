using System;
using System.Collections.Generic;
using System.Linq;
using b2.Domain.Core;

namespace b2.Domain.Tests
{
    public class InMemoryEventStorage : IEventStorage
    {
        private readonly List<Event> _storage = new List<Event>();

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events)
        {
            _storage.AddRange(events);
        }

        public IReadOnlyCollection<Event> GetAll(Guid aggregateId)
        {
            return _storage.Where(x => x.Id == aggregateId).ToList().AsReadOnly();
        }
    }
}