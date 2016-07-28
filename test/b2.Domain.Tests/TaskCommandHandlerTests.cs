using System.Linq;
using b2.Domain.Core;

namespace b2.Domain.Tests
{
    public class TaskCommandHandlerTests
    {
        private readonly InMemoryRepository _repository;
        private readonly TaskCommandHandler _handler;

         public TaskCommandHandlerTests()
        {
            _repository = new InMemoryRepository();
            _handler = new TaskCommandHandler(_repository);
        }
        
        public void CreateTask()
        {
            var id = "task-id";
            var name = "task";
            var url = "http://task";
            var status = "new";

            var command = new CreateTaskCommand(id, name, url, status);

            _handler.Handle(command);

            var @event = GetFromRepository<TaskCreated>(id);
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