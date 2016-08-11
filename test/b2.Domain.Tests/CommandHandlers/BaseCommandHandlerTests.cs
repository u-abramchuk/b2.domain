using System;
using System.Linq;
using b2.Domain.Core;

namespace b2.Domain.Tests.CommandHandlers
{
    public class BaseCommandHandlerTests
    {

        public BaseCommandHandlerTests()
        {
            Publisher = new InMemoryEventPublisher();
            Storage = new InMemoryEventStore();
            Repository = new Repository(Storage, Publisher);
        }

        protected InMemoryEventPublisher Publisher { get; }

        protected InMemoryEventStore Storage { get; }
        protected Repository Repository { get; }

        protected TEvent GetStoredEvent<TEvent>(Guid id) where TEvent : Event
        {
            return Storage.GetAllSync(id)
                .Select(x => x.Event)
                .OfType<TEvent>()
                .Single();
        }

        protected TEvent GetPublishedEvent<TEvent>(Guid id) where TEvent : Event
        {
            return Publisher.PublishedEvents
                .Select(x => x.Event)
                .Where(x => x.Id == id)
                .OfType<TEvent>()
                .Single();
        }
    }
}