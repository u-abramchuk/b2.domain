using System;
using System.Collections.Generic;
using System.Text;
using b2.Domain.Core;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace b2.Domain.Web
{
    public class EventPublisher : IEventPublisher, IDisposable
    {
        private readonly ConnectionFactory _factory;

        public EventPublisher(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public void Publish(IEnumerable<EventDescriptor> events)
        {
            using (var connection = _factory.CreateConnection())
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

        public void Dispose()
        {
        }
    }
}