using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using b2.Domain.Core;
using b2.Domain.Events;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace b2.Domain
{
    public class PersistentEventStore : IEventStore, IDisposable
    {
        private readonly Type[] _knownEvents = new[] {
            typeof(TaskCreated),
            typeof(BranchCreated),
            typeof(WorkItemCreatedFromBranch),
            typeof(WorkItemCreatedFromTask),
            typeof(TaskAssignedToWorkItem),
            typeof(BranchAssignedToWorkItem)
        };

        private readonly IEventStoreConnection _connection;

        public PersistentEventStore(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task SaveEvents(Guid aggregateId, IEnumerable<Event> events)
        {
            var stream = aggregateId.ToString();

            await _connection.AppendToStreamAsync(
                stream,
                ExpectedVersion.Any,
                events.Select(ConvertEventToEventData)
            );
        }

        public async Task<IReadOnlyCollection<Event>> GetAll(Guid aggregateId)
        {
            var stream = aggregateId.ToString();
            var allEvents = new List<ResolvedEvent>();
            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;

            do
            {
                currentSlice = await _connection
                .ReadStreamEventsForwardAsync(stream, nextSliceStart, 200, false);

                nextSliceStart = currentSlice.NextEventNumber;

                allEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            return allEvents
                .Select(x => ConvertRecordedEventToEvent(x.Event))
                .ToList();
        }

        private EventData ConvertEventToEventData(Event @event)
        {
            var serializedBody = JsonConvert.SerializeObject(@event);
            return new EventData(
                Guid.NewGuid(),
                @event.GetType().Name,
                true,
                Encoding.UTF8.GetBytes(serializedBody),
                null
            );
        }

        private Event ConvertRecordedEventToEvent(RecordedEvent @event)
        {
            var body = Encoding.UTF8.GetString(@event.Data);
            var type = _knownEvents.SingleOrDefault(x => x.Name == @event.EventType);

            return (Event)JsonConvert.DeserializeObject(body, type);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}