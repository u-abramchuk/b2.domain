using System;

namespace b2.Domain.Core
{
    public class Repository
    {
        public Repository(IEventStorage storage)
        {
            Storage = storage;
        }

        public IEventStorage Storage { get; }

        public T GetById<T>(Guid aggregateId) where T : AggregateRoot, new()
        {
            var events = Storage.GetAll(aggregateId);
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

            Storage.SaveEvents(aggregate.Id, events);
            aggregate.MarkChangesAsCommited();
        }
    }
}