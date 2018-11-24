namespace MyCompany.Infrastructure.MessageBus
{
    public class Message
    {
        public Message(string topic, object payload)
        {
            this.Topic = topic;
            this.Payload = payload;

        }
        public string Topic { get; }
        public object Payload { get; }
    }
}