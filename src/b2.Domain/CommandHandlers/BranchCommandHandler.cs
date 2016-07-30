using b2.Domain.Commands;
using b2.Domain.Core;

namespace b2.Domain.CommandHandlers
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

    
}