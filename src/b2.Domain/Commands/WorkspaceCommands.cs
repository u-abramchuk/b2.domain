using System;

namespace b2.Domain.Commands
{
    public class CreateWorkspaceCommand
    {
        public CreateWorkspaceCommand(string name, Guid? id = null)
        {
            Id = id;
            Name = name;
        }

        public Guid? Id { get; }
        public string Name { get; }
    }
}