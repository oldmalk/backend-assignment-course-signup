using System;
using System.Threading.Tasks;

namespace MyCompany.Infrastructure.MessageBus
{
    public interface IMessageBus
    {
         void Publish(Message message);

         void Listen<T>(string queueName, Action<T> callback) where T : Message;
    }
}