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
        private readonly string _connectionString;
        private readonly KnownEvents _knownEvents;
        private readonly JsonSerializer _serializer;
        private IEventStoreConnection _connection;

        public EventStore(
            string connectionString,
            KnownEvents knownEvents,
            JsonSerializer serializer
        )
        {
            _connectionString = connectionString;
            _knownEvents = knownEvents;
            _serializer = serializer;

            InitializeConnection();
        }

        private void InitializeConnection()
        {
            _connection = EventStoreConnection.Create(_connectionString).Result;

            _connection.Closed += (e, args) => Connect();

            Connect();
        }

        private void Connect()
        {
            _connection.ConnectAsync().Wait();
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
                currentSlice = await _connection.ReadStreamEventsForwardAsync(
                        stream,
                        nextSliceStart,
                        200,
                        false
                );

                nextSliceStart = currentSlice.NextEventNumber;

                allEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            return allEvents
                .Select(x => ConvertRecordedEventToEventDescriptor(x.Event))
                .ToList();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        private EventData ConvertEventDescriptorToEventData(EventDescriptor eventDescriptor)
        {
            var serializedBody = _serializer.Serialize(eventDescriptor.Event);
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
            var type = _knownEvents.FindTypeByTypeName(@event.EventType);

            return new EventDescriptor(
                @event.EventId,
                @event.EventType,
                @event.EventNumber,
                (Event)JsonConvert.DeserializeObject(body, type)
            );
        }
    }
}