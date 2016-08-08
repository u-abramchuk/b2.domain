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

            var storedEventsCount = Storage.GetAllSync(workItemFromBranchId).Count();

            Assert.Equal(2, storedEventsCount);
        }

        [Fact]
        public async Task CreateWorkItemFromTask()
        {
            var id = Guid.NewGuid();
            var command = new CreateWorkItemFromTaskCommand(id, taskId);

            await _handler.Handle(command);

            var storedEvent = GetStoredEvent<WorkItemCreatedFromTask>(id);

            Assert.Equal(id, storedEvent.Id);
            Assert.Equal(taskId, storedEvent.TaskId);

            Assert.Equal(id, @event.Id);
            Assert.Equal(taskId, @event.TaskId);
        }

        [Fact]
        public async Task CreateWorkItemFromBranch()
        {
            var id = Guid.NewGuid();
            var command = new CreateWorkItemFromBranchCommand(id, branchId);

            await _handler.Handle(command);

            var storedEvent = GetStoredEvent<WorkItemCreatedFromBranch>(id);

            Assert.Equal(id, storedEvent.Id);
            Assert.Equal(branchId, storedEvent.BranchId);

            Assert.Equal(id, @event.Id);
            Assert.Equal(branchId, @event.BranchId);
        }

        [Fact]
        public async Task AssignTaskToWorkItem()
        {
            var command =
                new AssignTaskToWorkItemCommand(workItemFromBranchId, taskId);

            await _handler.Handle(command);

            var storedEvent = GetStoredEvent<TaskAssignedToWorkItem>(workItemFromBranchId);

            Assert.Equal(workItemFromBranchId, storedEvent.Id);
            Assert.Equal(taskId, storedEvent.TaskId);

            Assert.Equal(workItemFromBranchId, @event.Id);
            Assert.Equal(taskId, @event.TaskId);
        }

        private void PopulateRepository()
        {
            var taskCreatedEvent = new TaskCreated(taskId, "task", "http://task", "new");
            var taskCreatedEventDescriptor = new EventDescriptor(taskCreatedEvent);
            Storage.SaveEventsSync(taskId, new [] { taskCreatedEventDescriptor });

            var branchCreatedEvent = new BranchCreated(branchId);
            var branchCreatedEventDescriptor= new EventDescriptor(branchCreatedEvent);
            Storage.SaveEventsSync(branchId, new [] { branchCreatedEventDescriptor });

            var workItemCreatedFromBranch =
                new WorkItemCreatedFromBranch(workItemFromBranchId, branchId);
            var workItemCreatedFromBranchDescriptor = 
                new EventDescriptor(workItemCreatedFromBranch); 
            Storage.SaveEventsSync(
                workItemFromBranchId, 
                new [] { workItemCreatedFromBranchDescriptor }
            );
        }
    }
}