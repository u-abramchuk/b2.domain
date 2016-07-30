namespace b2.Domain.Commands
{
    public class CreateBranchCommand
    {
        public CreateBranchCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}