using System.Linq;
using b2.Domain.Core;
using Xunit;

namespace b2.Domain.Tests
{
    class BranchCommandHandlerTests
    {
        private readonly InMemoryRepository _repository;
        private readonly BranchCommandHandler _handler;

        public BranchCommandHandlerTests()
        {
            _repository = new InMemoryRepository();
            _handler = new BranchCommandHandler(_repository);
        }

        [Fact]
        public void CreateBranch()
        {
            var id = "branch-id";
            var command = new CreateBranchCommand(id);

            _handler.Handle(command);

            var @event = GetFromRepository<BranchCreated>(id);

            Assert.Equal(id, @event.Id);
        }

        private TEvent GetFromRepository<TEvent>(string id)
            where TEvent : Event
        {
            return _repository.Storage
                .OfType<TEvent>()
                .Where(x => x.Id == id)
                .Single();
        }
    }
}