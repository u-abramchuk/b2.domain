using b2.Domain.Core;

namespace b2.Domain
{
    public class WorkItemCommandHandler
    {
        private readonly IRepository _repository;

        public WorkItemCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateWorkItemFromTaskCommand command)
        {
            var task = _repository.GetById<Task>(command.TaskId);

            var workItem = new WorkItem(command.WorkItemId, task);

            _repository.Save(workItem);
        }

        public void Handle(CreateWorkItemFromBranchCommand command)
        {
            var branch = _repository.GetById<Branch>(command.BranchId);

            var workItem = new WorkItem(command.Id, branch);

            _repository.Save(workItem);
        }

        public void Handle(AssignTaskToWorkItemCommand command)
        {
            var task = _repository.GetById<Task>(command.TaskId);
            var workItem = _repository.GetById<WorkItem>(command.Id);

            workItem.AssignTask(task);

            _repository.Save(workItem);
        }
    }
}