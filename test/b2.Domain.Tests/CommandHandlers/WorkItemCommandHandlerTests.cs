using Xunit;
using System.Linq;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using b2.Domain.Events;
using System;
using System.Threading.Tasks;

namespace b2.Domain.Tests.CommandHandlers
{
    public class WorkItemCommandHandlerTests : BaseCommandHandlerTests
    {

        private readonly WorkItemCommandHandler _handler;

        private Guid workItemFromBranchId = Guid.NewGuid();
        private Guid taskId = Guid.NewGuid();
        private Guid branchId = Guid.NewGuid();

        public WorkItemCommandHandlerTests()
        {
            _handler = new WorkItemCommandHandler(Repository);

            PopulateRepository();
        }

        [Fact]
        public async Task SaveOnlyNewEvents()
        {
            var command =
                new AssignTaskToWorkItemCommand(workItemFromBranchId, taskId);

            await _handler.Handle(command);

            var eventsCount = Storage.GetAllSync(workItemFromBranchId).Count();

            Assert.Equal(2, eventsCount);
        }

        [Fact]
        public async Task CreateWorkItemFromTask()
        {
            var id = Guid.NewGuid();
            var command = new CreateWorkItemFromTaskCommand(id, taskId);

            await _handler.Handle(command);

            var @event = GetFromRepository<WorkItemCreatedFromTask>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(taskId, @event.TaskId);
        }

        [Fact]
        public async Task CreateWorkItemFromBranch()
        {
            var id = Guid.NewGuid();
            var command = new CreateWorkItemFromBranchCommand(id, branchId);

            await _handler.Handle(command);

            var @event = GetFromRepository<WorkItemCreatedFromBranch>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(branchId, @event.BranchId);
        }

        [Fact]
        public async Task AssignTaskToWorkItem()
        {
            var command =
                new AssignTaskToWorkItemCommand(workItemFromBranchId, taskId);

            await _handler.Handle(command);

            var @event = GetFromRepository<TaskAssignedToWorkItem>(workItemFromBranchId);

            Assert.Equal(workItemFromBranchId, @event.Id);
            Assert.Equal(taskId, @event.TaskId);
        }

        private void PopulateRepository()
        {
            var taskCreatedEvent = new TaskCreated(taskId, "task", "http://task", "new");
            Storage.SaveEventsSync(taskId, new[] { taskCreatedEvent });

            var branchCreatedEvent = new BranchCreated(branchId);
            Storage.SaveEventsSync(branchId, new[] { branchCreatedEvent });

            var workItemCreatedFromBranch =
                new WorkItemCreatedFromBranch(workItemFromBranchId, branchId);
            Storage.SaveEventsSync(workItemFromBranchId, new[] { workItemCreatedFromBranch });
        }
    }
}