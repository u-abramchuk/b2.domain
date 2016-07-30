namespace b2.Domain.Commands
{
    public class CreateTaskCommand
    {
        public CreateTaskCommand(string id, string name, string url, string status)
        {
            Id = id;
            Name = name;
            Url = url;
            Status = status;
        }

        public string Id { get; }
        public string Name { get; }
        public string Url { get; }
        public string Status { get; }
    }
}