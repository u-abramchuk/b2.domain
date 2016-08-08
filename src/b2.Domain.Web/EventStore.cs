using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using b2.Domain.Core;
using b2.Domain.Events;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace b2.Domain
{
    public class EventStore : IEventStore, IDisposable
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

        public EventStore(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task SaveEvents(
            Guid aggregateId,
            IEnumerable<EventDescriptor> eventDescriptors
        )
        {
            var stream = aggregateId.ToString();

            await _connection.AppendToStreamAsync(
                stream,
                ExpectedVersion.Any,
                eventDescriptors.Select(ConvertEventDescriptorToEventData)
            );
        }

        public async Task<IReadOnlyCollection<EventDescriptor>> GetAll(Guid aggregateId)
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
                .Select(x => ConvertRecordedEventToEventDescriptor(x.Event))
                .ToList();
        }

        private EventData ConvertEventDescriptorToEventData(EventDescriptor eventDescriptor)
        {
            var serializedBody = JsonConvert.SerializeObject(eventDescriptor.Event);
            return new EventData(
                eventDescriptor.Id,
                eventDescriptor.EventType,
                true,
                Encoding.UTF8.GetBytes(serializedBody),
                null
            );
        }

        private EventDescriptor ConvertRecordedEventToEventDescriptor(RecordedEvent @event)
        {
            var body = Encoding.UTF8.GetString(@event.Data);
            var type = _knownEvents.SingleOrDefault(x => x.Name == @event.EventType);

            return new EventDescriptor(
                @event.EventId,
                @event.EventType,
                @event.EventNumber,
                (Event) JsonConvert.DeserializeObject(body, type)
            );
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}