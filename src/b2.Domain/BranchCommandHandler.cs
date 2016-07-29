using b2.Domain.Core;

namespace b2.Domain
{
    public class BranchCommandHandler
    {
        private readonly IRepository _repository;
        public BranchCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateBranchCommand command)
        {
            var branch = new Branch(command.Id);

            _repository.Save(branch);
        }
    }

    public class CreateBranchCommand
    {
        public CreateBranchCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}