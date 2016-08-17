using System;
using System.Collections.Generic;
using System.Text;
using b2.Domain.Core;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace b2.Domain.Web
{
    public class EventPublisher : IEventPublisher
    {
        private Lazy<ConnectionFactory> _factory;

        public EventPublisher(string connectionString)
        {
            _factory = new Lazy<ConnectionFactory>(() => new ConnectionFactory
            {
                Uri = connectionString,
                AutomaticRecoveryEnabled = true
            });
        }

        public void Publish(IEnumerable<EventDescriptor> events)
        {
            using (var connection = GetFactory().CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "b2.domain.events",
                                                 durable: true,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);
                foreach (var @event in events)
                {
                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: "b2.domain.events",
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }

        private ConnectionFactory GetFactory()
        {
            return _factory.Value;
        }
    }
}