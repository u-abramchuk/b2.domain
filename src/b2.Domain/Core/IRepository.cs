using System.Collections.Generic;
using System.Linq;

namespace b2.Domain.Core
{
    public interface IRepository
    {
        T GetById<T>(string id) where T : AggregateRoot, new();
        void Save<T>(T aggregate) where T : AggregateRoot, new();
    }

    public class InMemoryRepository : IRepository
    {
        public InMemoryRepository()
        {
            Storage = new List<Event>();
        }

        public List<Event> Storage { get; }

        public T GetById<T>(string id) where T : AggregateRoot, new()
        {
            var events = Storage.Where(x => x.Id == id);
            var result = new T();

            foreach (var @event in events)
            {
                result.HandleEvent(@event, false);
            }
            return result;
        }

        public void Save<T>(T aggregate) where T : AggregateRoot, new()
        {
            var events = aggregate.Changes;

            foreach (var @event in events)
            {
                Storage.Add(@event);
            }

            aggregate.MarkChangesAsCommited();
        }
    }
}