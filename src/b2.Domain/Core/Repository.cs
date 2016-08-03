using System;
using System.Threading.Tasks;

namespace b2.Domain.Core
{
    public class Repository
    {
        public Repository(IEventStorage storage)
        {
            Storage = storage;
        }

        public IEventStorage Storage { get; }

        public async Task<T> GetById<T>(Guid aggregateId) where T : AggregateRoot, new()
        {
            var result = new T();
            var events = await Storage.GetAll(aggregateId);

            foreach (var @event in events)
            {
                result.HandleEvent(@event, false);
            }
            return result;
        }

        public async Task Save<T>(T aggregate) where T : AggregateRoot, new()
        {
            var events = aggregate.Changes;

            await Storage.SaveEvents(aggregate.Id, events);
            aggregate.MarkChangesAsCommited();
        }
    }
}