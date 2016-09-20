using System.Collections.Generic;
using System.Linq;
using System.Text;
using b2.Domain.Core;
using b2.Domain.Events;
using RabbitMQ.Client;

namespace b2.Domain.Web
{
    public class EventPublisher : IEventPublisher
    {
        private readonly JsonSerializer _serializer;
        private readonly KnownEvents _knownEvents;
        private ConnectionFactory _factory;

        public EventPublisher(
            string connectionString,
            JsonSerializer serializer,
            KnownEvents knownEvents
            )
        {
            _serializer = serializer;
            _knownEvents = knownEvents;
            _factory = new ConnectionFactory
            {
                Uri = connectionString,
                AutomaticRecoveryEnabled = true
            };
        }

        public void Publish(IEnumerable<EventDescriptor> events)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(
                    exchange: "b2.domain.events",
                    type: "topic",
                    durable: true
                );

                channel.QueueDeclare(queue: "b2.domain.events",
                                                 durable: true,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);
                foreach (var key in _knownEvents.Keys)
                {
                    channel.QueueBind(
                        queue: "b2.domain.events",
                        exchange: "b2.domain.events",
                        routingKey: GetRoutingKey(key)
                    );
                }

                foreach (var @event in events)
                {
                    var message = _serializer.Serialize(@event);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "b2.domain.events",
                        routingKey: GetRoutingKey(@event),
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }

        private string GetRoutingKey(string key)
        {
            return $"domain.{key}";
        }

        private string GetRoutingKey(EventDescriptor @event)
        {
            var eventKey = _knownEvents.GetEventKeyByTypeName(@event.EventType);
            
            return GetRoutingKey(eventKey);
        }
    }
}