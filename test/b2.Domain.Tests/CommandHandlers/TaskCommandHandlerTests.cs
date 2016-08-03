using System;
using System.Threading.Tasks;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using b2.Domain.Events;
using Xunit;

namespace b2.Domain.Tests.CommandHandlers
{
    public class TaskCommandHandlerTests : BaseCommandHandlerTests
    {
        private readonly TaskCommandHandler _handler;

         public TaskCommandHandlerTests()
        {
            _handler = new TaskCommandHandler(Repository);
        }
        
        public async Task CreateTask()
        {
            var id = Guid.NewGuid();
            var name = "task";
            var url = "http://task";
            var status = "new";

            var command = new CreateTaskCommand(id, name, url, status);

            await _handler.Handle(command);

            var @event = GetFromRepository<TaskCreated>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(name, @event.Name);
            Assert.Equal(url, @event.Url);
            Assert.Equal(status, @event.Status);
        }
    }
}