using System;
using b2.Domain.Commands;
using b2.Domain.Core;
using b2.Domain.Entities;

namespace b2.Domain.CommandHandlers
{
    public class WorkspaceCommandHandler
    {
        private readonly Repository _repository;

        public WorkspaceCommandHandler(Repository repository)
        {
            _repository = repository;
        }

        public async System.Threading.Tasks.Task<Guid> Handle(CreateWorkspaceCommand command)
        {
            var id = command.Id ?? Guid.NewGuid();
            var workspace = new Workspace(id, command.Name);

            await _repository.Save(workspace);

            return id;
        }
    }
}