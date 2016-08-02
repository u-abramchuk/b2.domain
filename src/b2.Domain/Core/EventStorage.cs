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