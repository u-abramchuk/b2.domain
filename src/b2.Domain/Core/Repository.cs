using System;
using System.Linq;
using System.Threading.Tasks;
using b2.Domain.Events;

namespace b2.Domain.Core
{
    public class Repository
    {
        private readonly IEventStore _store;
        private readonly IEventPublisher _publisher;

        public Repository(IEventStore store, IEventPublisher publisher)
        {
            _store = store;
            _publisher = publisher;
        }

        public async Task<T> GetById<T>(Guid aggregateId) where T : AggregateRoot, new()
        {
            var result = new T();

            var events = await _store.GetAll(aggregateId);

            foreach (var @event in events)
            {
                result.HandleEvent(@event.Event, false);
            }

            return result;
        }

        public async Task Save<T>(T aggregate) where T : AggregateRoot, new()
        {
            var events = aggregate.Changes
                .Select(@event => new EventDescriptor(
                    Guid.NewGuid(),
                    @event.GetType().Name,
                    -1,
                    @event)
                );

            await _store.SaveEvents(aggregate.Id, events);
            _publisher.Publish(events);

            aggregate.MarkChangesAsCommited();
        }
    }
}