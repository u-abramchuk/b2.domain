using System;
using System.Linq;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using b2.Domain.Core;
using b2.Domain.Events;
using Xunit;

namespace b2.Domain.Tests.CommandHandlers
{
    class BranchCommandHandlerTests
    {
        private readonly InMemoryEventStorage _storage;
        private readonly Repository _repository;
        private readonly BranchCommandHandler _handler;

        public BranchCommandHandlerTests()
        {
            _storage = new InMemoryEventStorage();
            _repository = new Repository(_storage);
            _handler = new BranchCommandHandler(_repository);
        }

        [Fact]
        public void CreateBranch()
        {
            var id = Guid.NewGuid();
            var command = new CreateBranchCommand(id);

            _handler.Handle(command);

            var @event = GetFromRepository<BranchCreated>(id);

            Assert.Equal(id, @event.Id);
        }

        private TEvent GetFromRepository<TEvent>(Guid id)
            where TEvent : Event
        {
            return _storage.GetAll(id)
                .OfType<TEvent>()
                .Single();
        }
    }
}