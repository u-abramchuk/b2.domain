using System;
using b2.Domain.Core;

namespace b2.Domain.Commands
{
    public class CreateWorkspaceCommand : Command
    {
        public CreateWorkspaceCommand(string name, string creatorId, Guid? id = null)
        {
            Id = id;
            Name = name;
            CreatorId = creatorId;
        }

        public Guid? Id { get; }
        public string Name { get; }
        public string CreatorId { get; }
    }
}