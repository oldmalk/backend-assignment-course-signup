using System.Threading.Tasks;
using MyCompany.Domain;

namespace MyCompany.Domain.Events
{
    public interface IEventProcessor
    {
         Task Process(DomainEvent domainEvent);
    }
}