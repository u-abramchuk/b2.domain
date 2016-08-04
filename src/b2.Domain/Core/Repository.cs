using System;
using System.Threading.Tasks;

namespace b2.Domain.Core
{
    public class Repository
    {
        public Repository(IEventStore store)
        {
            Store = store;
        }

        public IEventStore Store { get; }

        public async Task<T> GetById<T>(Guid aggregateId) where T : AggregateRoot, new()
        {
            var result = new T();
            var events = await Store.GetAll(aggregateId);

            foreach (var @event in events)
            {
                result.HandleEvent(@event, false);
            }
            return result;
        }

        public async Task Save<T>(T aggregate) where T : AggregateRoot, new()
        {
            var events = aggregate.Changes;

            await Store.SaveEvents(aggregate.Id, events);
            aggregate.MarkChangesAsCommited();
        }
    }
}