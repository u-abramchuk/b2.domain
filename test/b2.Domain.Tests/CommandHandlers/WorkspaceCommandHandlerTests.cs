using System;
using System.Threading.Tasks;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using b2.Domain.Events;
using Xunit;

namespace b2.Domain.Tests.CommandHandlers
{
    public class WorkspaceCommandHandlerTests : BaseCommandHandlerTests
    {
        private readonly WorkspaceCommandHandler _handler;

        public WorkspaceCommandHandlerTests()
        {
            _handler = new WorkspaceCommandHandler(Repository);
        }

        [Fact]
        public async Task CreateWorkspace()
        {
            var id = Guid.NewGuid();
            var name = "workspace";
            var command = new CreateWorkspaceCommand(name, id);

            await _handler.Handle(command);

            var storedEvent = GetStoredEvent<WorkspaceCreated>(id);

            Assert.Equal(id, storedEvent.Id);
            Assert.Equal(name, storedEvent.Name);

            var publishedEvent = GetPublishedEvent<WorkspaceCreated>(id);

            Assert.Equal(id, publishedEvent.Id);
            Assert.Equal(name, publishedEvent.Name);
        }

        [Fact]
        public async Task CreateWorkspaceWithoutSpecifyingId()
        {
            var name = "workspace";
            var command = new CreateWorkspaceCommand(name);

            var id = await _handler.Handle(command);

            var storedEvent = GetStoredEvent<WorkspaceCreated>(id);

            Assert.Equal(id, storedEvent.Id);
            Assert.Equal(name, storedEvent.Name);

            var publishedEvent = GetPublishedEvent<WorkspaceCreated>(id);

            Assert.Equal(id, publishedEvent.Id);
            Assert.Equal(name, publishedEvent.Name);
        }
    }
}