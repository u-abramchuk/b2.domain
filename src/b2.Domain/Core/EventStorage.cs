using System.Collections.Generic;

namespace b2.Domain.Core
{
    public interface IEventStorage
    {
        void Add(Event @event);
        IReadOnlyCollection<Event> GetAll();
    }

    public class InMemoryEventStorage : IEventStorage
    {
        private readonly List<Event> _storage = new List<Event>();

        public void Add(Event @event)
        {
            _storage.Add(@event);
        }

        public IReadOnlyCollection<Event> GetAll()
        {
            return _storage.AsReadOnly();
        }
    }
}