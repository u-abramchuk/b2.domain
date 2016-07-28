namespace b2.Domain.Core
{
    public class Event
    {
        public Event(string id)
        {
            Id = id;
        }
        
        public string Id { get; }
    }
}