using b2.Domain.Core;
using b2.Domain.Events;

namespace b2.Domain.Entities
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
}