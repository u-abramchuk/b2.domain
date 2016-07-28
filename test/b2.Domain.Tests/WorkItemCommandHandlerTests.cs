using Xunit;
using b2.Domain.Core;
using System.Linq;

namespace b2.Domain.Tests
{
    public class WorkItemCommandHandlerTests
    {
        private readonly InMemoryRepository _repository;
        private readonly WorkItemCommandHandler _handler;

        private const string workItemFromBranchId = "workitem-with-branch-id";
        private const string taskId = "task-id";

        public WorkItemCommandHandlerTests()
        {
            _repository = new InMemoryRepository();
            _handler = new WorkItemCommandHandler(_repository);

            PopulateRepository();
        }

        [Fact]
        public void SaveOnlyNewEvents()
        {
            var command =
                new AssignTaskToWorkItemCommand(workItemFromBranchId, taskId);

            _handler.Handle(command);

            var eventsCount = _repository.Storage
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