using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MyCompany.Infrastructure.MessageBus
{
    public class RabbitMessageBus : IMessageBus
    {
        private readonly IModel _channel;
        private const string EXCHANGE_NAME = "courses.topic";

        public RabbitMessageBus()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "message-bus",
                UserName = "guest",
                Password = "guest"
            };

            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Listen<T>(string queueName, Action<T> callback) where T : Message
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));
            
            CreateExchangeAndQueue(queueName);

            var eventConsumer = new EventingBasicConsumer(_channel);
            eventConsumer.Received += (sender, args) =>
            {
                var message = GetObject<T>(args.Body);

                callback(message);
                
                _channel.BasicAck(args.DeliveryTag, false);
            };

            _channel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: eventConsumer
            );
        }

        private void CreateExchangeAndQueue(string queueName)
        {
            _channel.ExchangeDeclare(
                exchange: EXCHANGE_NAME,
                type: "topic",
                durable: true,
                autoDelete: false
            );

            _channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            _channel.QueueBind(
                queue: queueName,
                exchange: EXCHANGE_NAME,
                routingKey: queueName
            );
        }

        public void Publish(Message message)
        {
            _channel.BasicPublish(
                exchange: EXCHANGE_NAME,
                routingKey: message.Topic,
                body: GetBytes(message));
        }

        private byte[] GetBytes(object payload)
        {
            var payloadString = JsonConvert.SerializeObject(payload);
            return Encoding.UTF8.GetBytes(payloadString);
        }

        private T GetObject<T>(byte[] buffer) where T : Message
        {
            var payloadString = Encoding.UTF8.GetString(buffer);
            return JsonConvert.DeserializeObject<T>(payloadString);
        }
    }
}