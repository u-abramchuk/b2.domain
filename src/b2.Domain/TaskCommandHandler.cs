using b2.Domain.Core;

namespace b2.Domain
{
    public class TaskCommandHandler
    {
        private readonly IRepository _repository;

        public TaskCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateTaskCommand command)
        {
            var task =  new Task(command.Id, command.Name, command.Url, command.Status);

            _repository.Save(task);
        }
    }
}