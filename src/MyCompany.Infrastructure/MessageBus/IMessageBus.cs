using System.Threading.Tasks;

namespace MyCompany.Infrastructure.MessageBus
{
    public interface IMessageBus
    {
         void Publish(Message message);
    }
}