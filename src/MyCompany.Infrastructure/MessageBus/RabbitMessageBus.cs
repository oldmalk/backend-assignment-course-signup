using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MyCompany.Infrastructure.MessageBus
{
    public class RabbitMessageBus : IMessageBus
    {
        private readonly IModel _channel;
        public RabbitMessageBus()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "amqp://message-bus",
                UserName = "guest",
                Password = "guest"
            };

            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Publish(Message message)
        {
            _channel.BasicPublish(
                exchange: "courses.topic",
                routingKey: message.Topic,
                body: GetBytes(message.Payload));
        }

        private byte[] GetBytes(object payload)
        {
            var payloadString = JsonConvert.SerializeObject(payload);
            return Encoding.UTF8.GetBytes(payloadString);
        }
    }
}