using System;
using System.Linq;
using b2.Domain.Core;

namespace b2.Domain.Tests.CommandHandlers
{
    public class BaseCommandHandlerTests
    {

        public BaseCommandHandlerTests()
        {
            Storage = new InMemoryEventStore();
            Repository = new Repository(Storage);
        }

        protected InMemoryEventStore Storage { get; }
        protected Repository Repository { get; }

        protected TEvent GetStoredEvent<TEvent>(Guid id) where TEvent : Event
        {
            return Storage.GetAllSync(id)
                .Select(x => x.Event)
                .OfType<TEvent>()
                .Single();
        }
                .OfType<TEvent>()
                .Single();
        }
    }
}