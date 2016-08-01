using Xunit;
using b2.Domain.Core;
using System.Linq;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using b2.Domain.Events;

namespace b2.Domain.Tests
{
    public class WorkItemCommandHandlerTests
    {
        private readonly InMemoryEventStorage _storage;
        private readonly Repository _repository;
        private readonly WorkItemCommandHandler _handler;

        private const string workItemFromBranchId = "workitem-with-branch-id";
        private const string taskId = "task-id";
        private const string branchId = "branch-id";

        public WorkItemCommandHandlerTests()
        {
            _storage = new InMemoryEventStorage();
            _repository = new Repository(_storage);
            _handler = new WorkItemCommandHandler(_repository);

            PopulateRepository();
        }

        [Fact]
        public void SaveOnlyNewEvents()
        {
            var command =
                new AssignTaskToWorkItemCommand(workItemFromBranchId, taskId);

            _handler.Handle(command);

            var eventsCount = _storage.GetAll()
                .Where(x => x.Id == workItemFromBranchId)
                .Count();

            Assert.Equal(2, eventsCount);
        }

        [Fact]
        public void CreateWorkItemFromTask()
        {
            var id = "new-id";
            var command = new CreateWorkItemFromTaskCommand(id, taskId);

            _handler.Handle(command);

            var @event = GetFromRepository<WorkItemCreatedFromTask>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(taskId, @event.TaskId);
        }

        [Fact]
        public void CreateWorkItemFromBranch()
        {
            var id = "new-id";
            var command = new CreateWorkItemFromBranchCommand(id, branchId);

            _handler.Handle(command);

            var @event = GetFromRepository<WorkItemCreatedFromBranch>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(branchId, @event.BranchId);
        }

        [Fact]
        public void AssignTaskToWorkItem()
        {
            var command =
                new AssignTaskToWorkItemCommand(workItemFromBranchId, taskId);

            _handler.Handle(command);

            var @event =
                GetFromRepository<TaskAssignedToWorkItem>(workItemFromBranchId);

            Assert.Equal(workItemFromBranchId, @event.Id);
            Assert.Equal(taskId, @event.TaskId);
        }

        private void PopulateRepository()
        {
            var taskCreatedEvent = new TaskCreated(taskId, "task", "http://task", "new");
            _repository.Storage.Add(taskCreatedEvent);

            var branchCreatedEvent = new BranchCreated(branchId);
            _repository.Storage.Add(branchCreatedEvent);

            var workItemCreatedFromBranch =
                new WorkItemCreatedFromBranch(workItemFromBranchId, branchId);
            _repository.Storage.Add(workItemCreatedFromBranch);
        }

        private TEvent GetFromRepository<TEvent>(string id)
            where TEvent : Event
        {
            return _storage.GetAll()
                .OfType<TEvent>()
                .Where(x => x.Id == id)
                .Single();
        }
    }
}