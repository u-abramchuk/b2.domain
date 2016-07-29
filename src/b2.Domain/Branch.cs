using b2.Domain.Core;

namespace b2.Domain
{
    public class Branch : AggregateRoot
    {
        public Branch()
        {
        }

        public Branch(string id)
        {
            HandleEvent(new BranchCreated(id), true);
        }

        public void Handle(BranchCreated @event)
        {
            Id = @event.Id;
        }
    }

    public class BranchCreated : Event
    {
        public BranchCreated(string id) : base(id)
        {
        }
    }
}