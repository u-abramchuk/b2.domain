using System;
using System.Linq;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using b2.Domain.Core;
using b2.Domain.Events;
using Xunit;

namespace b2.Domain.Tests.CommandHandlers
{
    public class TaskCommandHandlerTests
    {
        private readonly InMemoryEventStorage _storage;
        private readonly Repository _repository;
        private readonly TaskCommandHandler _handler;

         public TaskCommandHandlerTests()
        {
            _storage = new InMemoryEventStorage();
            _repository = new Repository(_storage);
            _handler = new TaskCommandHandler(_repository);
        }
        
        public void CreateTask()
        {
            var id = Guid.NewGuid();
            var name = "task";
            var url = "http://task";
            var status = "new";

            var command = new CreateTaskCommand(id, name, url, status);

            _handler.Handle(command);

            var @event = GetFromRepository<TaskCreated>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(name, @event.Name);
            Assert.Equal(url, @event.Url);
            Assert.Equal(status, @event.Status);
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