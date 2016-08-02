using Xunit;
using b2.Domain.Core;
using System.Linq;
using b2.Domain.CommandHandlers;
using b2.Domain.Commands;
using b2.Domain.Events;
using System;

namespace b2.Domain.Tests.CommandHandlers
{
    public class WorkItemCommandHandlerTests
    {
        private readonly InMemoryEventStorage _storage;
        private readonly Repository _repository;
        private readonly WorkItemCommandHandler _handler;

        private Guid workItemFromBranchId = Guid.NewGuid();
        private Guid taskId = Guid.NewGuid();
        private Guid branchId = Guid.NewGuid();

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

            var eventsCount = _storage.GetAll(workItemFromBranchId).Count();

            Assert.Equal(2, eventsCount);
        }

        [Fact]
        public void CreateWorkItemFromTask()
        {
            var id = Guid.NewGuid();
            var command = new CreateWorkItemFromTaskCommand(id, taskId);

            _handler.Handle(command);

            var @event = GetFromRepository<WorkItemCreatedFromTask>(id);

            Assert.Equal(id, @event.Id);
            Assert.Equal(taskId, @event.TaskId);
        }

        [Fact]
        public void CreateWorkItemFromBranch()
        {
            var id = Guid.NewGuid();
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
            _repository.Storage.SaveEvents(taskId, new[] { taskCreatedEvent });

            var branchCreatedEvent = new BranchCreated(branchId);
            _repository.Storage.SaveEvents(branchId, new[] { branchCreatedEvent });

            var workItemCreatedFromBranch =
                new WorkItemCreatedFromBranch(workItemFromBranchId, branchId);
            _repository.Storage.SaveEvents(workItemFromBranchId,
                new[] { workItemCreatedFromBranch });
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