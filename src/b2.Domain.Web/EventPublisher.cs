using System.Collections.Generic;
using System.Text;
using b2.Domain.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;

namespace b2.Domain.Web
{
    public class EventPublisher : IEventPublisher
    {
        private ConnectionFactory _factory;

        public EventPublisher(string connectionString)
        {
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
                channel.ExchangeDeclare(exchange: "b2.domain.events", type: "topic", durable: true);
                channel.QueueDeclare(queue: "b2.domain.events",
                                                 durable: true,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);
                channel.QueueBind("b2.domain.events", "b2.domain.events", "b2.domain.events");

                foreach (var @event in events)
                {
                    var message = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "b2.domain.events",
                        routingKey: "b2.domain.events",
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}