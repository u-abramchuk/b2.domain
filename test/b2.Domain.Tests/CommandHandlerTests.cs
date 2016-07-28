using Xunit;
using b2.Domain.Core;
using System.Linq;

namespace b2.Domain.Tests
{
    public class CommandHandlerTests
    {
        private readonly InMemoryRepository _repository;
        private readonly CommandHandler _handler;

        private const string workItemFromBranchId = "workitem-with-branch-id";

        public CommandHandlerTests()
        {
            _repository = new InMemoryRepository();
            _handler = new CommandHandler(_repository);

            PopulateRepository();
        }

        [Fact]
        public void SaveOnlyNewEvents()
        {
            var task = new Task("task-id", "task", "http://task", "new");
            var command =
                new AssignTaskToWorkItemCommand(workItemFromBranchId, task);

            _handler.Handle(command);

            var eventsCount = _repository.Storage
                .Where(x => x.Id == workItemFromBranchId)
                .Count();

            Assert.Equal(2, eventsCount);
        }

        [Fact]
        public void CreateWorkItemFromTest()
        {
            var id = "new-id";
            var task = new Task("task-id", "task", "http://task", "new");
            var command = new CreateWorkItemFromTaskCommand(id, task);

            _handler.Handle(command);

            var @event = GetFromRepository<WorkItemCreatedFromTask>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(task, @event.Task);
        }

        [Fact]
        public void CreateWorkItemFromBranch()
        {
            var id = "new-id";
            var branch = new Branch("branch-id");
            var command = new CreateWorkItemFromBranchCommand(id, branch);

            _handler.Handle(command);

            var @event = GetFromRepository<WorkItemCreatedFromBranch>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(branch, @event.Branch);
        }

        [Fact]
        public void AssignTaskToWorkItem()
        {
            var task = new Task("task-id", "task", "http://task", "new");
            var command =
                new AssignTaskToWorkItemCommand(workItemFromBranchId, task);

            _handler.Handle(command);

            var @event =
                GetFromRepository<TaskAssignedToWorkItem>(workItemFromBranchId);

            Assert.Equal(workItemFromBranchId, @event.Id);
            Assert.Equal(task, @event.Task);
        }

        private void PopulateRepository()
        {
            var branch = new Branch("branch-id");
            var workItemCreatedFromBranch =
                new WorkItemCreatedFromBranch(workItemFromBranchId, branch);
            _repository.Storage.Add(workItemCreatedFromBranch);
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