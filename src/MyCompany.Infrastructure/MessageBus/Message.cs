namespace MyCompany.Infrastructure.MessageBus
{
    public abstract class Message
    {
        protected Message(string topic)
        {
            this.Topic = topic;
        }

        public string Topic { get; }
    }
}