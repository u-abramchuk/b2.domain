using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace b2.Domain.Core
{
    public abstract class AggregateRoot : Entity
    {
        private List<Event> _changes = new List<Event>();

        protected AggregateRoot()
        {
        }

        public void HandleEvent(Event @event, bool isNew)
        {
            var handler = this.GetType()
                .GetTypeInfo()
                .DeclaredMethods
                .Single(x => x.Name == "Handle" &&
                    x.GetParameters().Single().ParameterType.Equals(@event.GetType()));

            handler.Invoke(this, new[] { @event });

            if (isNew)
            {
                _changes.Add(@event);
            }
        }

        public IReadOnlyCollection<Event> Changes
        {
            get
            {
                return _changes.AsReadOnly();
            }
        }

        internal void MarkChangesAsCommited()
        {
            _changes.Clear();
        }
    }
}