using b2.Domain.Core;

namespace b2.Domain.Events
{
    public class BranchCreated : Event
    {
        public BranchCreated(string id) : base(id)
        {
        }
    }
}