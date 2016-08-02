using System;

namespace b2.Domain.Commands
{
    public class CreateBranchCommand
    {
        public CreateBranchCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}