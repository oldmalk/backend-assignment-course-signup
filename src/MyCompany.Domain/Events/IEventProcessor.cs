using MyCompany.Domain;

namespace MyCompany.Domain.Events
{
    public interface IEventProcessor
    {
         void Process(DomainEvent domainEvent);
    }
}