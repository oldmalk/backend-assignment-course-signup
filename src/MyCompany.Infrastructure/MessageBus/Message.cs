namespace MyCompany.Infrastructure.MessageBus
{
    public class Message
    {
        public string Topic { get; set; }
        public object Payload { get; set; }
    }
}