using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using b2.Domain.Core;
using b2.Domain.Events;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace b2.Domain.Web
{
    public class EventStore : IEventStore, IDisposable
    {
        private readonly object _lockObject = new object();
        private readonly Type[] _knownEvents = new[] {
            typeof(TaskCreated),
            typeof(BranchCreated),
            typeof(WorkItemCreatedFromBranch),
            typeof(WorkItemCreatedFromTask),
            typeof(TaskAssignedToWorkItem),
            typeof(BranchAssignedToWorkItem)
        };
        private readonly string _connectionString;
        private Lazy<IEventStoreConnection> _connection;

        public EventStore(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new Lazy<IEventStoreConnection>(() =>
            {
                var connection = EventStoreConnection.Create(_connectionString).Result;

                connection.ConnectAsync().Wait();

                return connection;
            });
        }

        public async Task SaveEvents(
            Guid aggregateId,
            IEnumerable<EventDescriptor> eventDescriptors
        )
        {
            var stream = aggregateId.ToString();

            await GetConnection().AppendToStreamAsync(
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
                currentSlice = await GetConnection()
                    .ReadStreamEventsForwardAsync(stream, nextSliceStart, 200, false);

                nextSliceStart = currentSlice.NextEventNumber;

                allEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            return allEvents
                .Select(x => ConvertRecordedEventToEventDescriptor(x.Event))
                .ToList();
        }

        private IEventStoreConnection GetConnection()
        {
            return _connection.Value;
        }

        public void Dispose()
        {
            if (_connection.IsValueCreated)
            {
                _connection.Value.Dispose();
            }
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
                (Event)JsonConvert.DeserializeObject(body, type)
            );
        }
    }
}