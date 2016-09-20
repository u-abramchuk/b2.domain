using System;
using System.Collections.Generic;
using System.Linq;

namespace b2.Domain.Events
{
    public class KnownEvents
    {
        private readonly IDictionary<string, Type> _knownEvents =
            new Dictionary<string, Type> {
                {"workspace.created", typeof(WorkspaceCreated)}
            };

        public string[] Keys => _knownEvents.Keys.ToArray();

        public Type FindTypeByTypeName(string typeName)
        {
            return _knownEvents
                .Where(x => x.Value.Name == typeName)
                .Select(x => x.Value)
                .SingleOrDefault();
        }

        public string GetEventKeyByTypeName(string typeName)
        {
            return _knownEvents
                .Where(x => x.Value.Name == typeName)
                .Select(x => x.Key)
                .SingleOrDefault();
        }
    }
}