using b2.Domain.Core;

namespace b2.Domain
{
    public class CommandHandler
    {
        private readonly IRepository _repository;

        public CommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateWorkItemFromTaskCommand command)
        {
            var workItem = new WorkItem(command.Id, command.Task);

            _repository.Save(workItem);
        }

        public void Handle(CreateWorkItemFromBranchCommand command)
        {
            var workItem = new WorkItem(command.Id, command.Branch);

            _repository.Save(workItem);
        }

        public void Handle(AssignTaskToWorkItemCommand command)
        {
            var workItem = _repository.GetById<WorkItem>(command.Id);

            workItem.AssignTask(command.Task);

            _repository.Save(workItem);
        }
    }
}