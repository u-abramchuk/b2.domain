using System;
using System.Threading.Tasks;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using b2.Domain.Events;
using Xunit;

namespace b2.Domain.Tests.CommandHandlers
{
    class BranchCommandHandlerTests : BaseCommandHandlerTests
    {
        private readonly BranchCommandHandler _handler;

        public BranchCommandHandlerTests()
        {
            _handler = new BranchCommandHandler(Repository);
        }

        [Fact]
        public async Task CreateBranch()
        {
            var id = Guid.NewGuid();
            var command = new CreateBranchCommand(id);

            await _handler.Handle(command);

            var storedEvent = GetStoredEvent<BranchCreated>(id);

            Assert.Equal(id, storedEvent.Id);
        }
    }
}