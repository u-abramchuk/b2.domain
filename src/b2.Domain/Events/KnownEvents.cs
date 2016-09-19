using System;

namespace b2.Domain.Events
{
    public class KnownEvents
    {
        private readonly Type[] _knownEvents = new[] {
            typeof(WorkspaceCreated)
            // typeof(TaskCreated),
            // typeof(BranchCreated),
            // typeof(WorkItemCreatedFromBranch),
            // typeof(WorkItemCreatedFromTask),
            // typeof(TaskAssignedToWorkItem),
            // typeof(BranchAssignedToWorkItem)
        };

        public Type[] Types => _knownEvents;
    }
}