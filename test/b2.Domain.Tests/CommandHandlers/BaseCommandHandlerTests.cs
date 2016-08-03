using System;
using System.Linq;
using b2.Domain.Core;

namespace b2.Domain.Tests.CommandHandlers
{
    public class BaseCommandHandlerTests
    {

        public BaseCommandHandlerTests()
        {
            Storage = new InMemoryEventStorage();
            Repository = new Repository(Storage);
        }

        protected InMemoryEventStorage Storage { get; }
        protected Repository Repository { get; }

        protected TEvent GetFromRepository<TEvent>(Guid id) where TEvent : Event
        {
            return Storage.GetAllSync(id)
                .OfType<TEvent>()
                .Single();
        }
    }
}