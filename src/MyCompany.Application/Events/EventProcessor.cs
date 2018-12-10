using System.Collections.Generic;
using MyCompany.Domain.Events;

namespace MyCompany.Application.Events
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IList<DomainEvent> _eventLog = new List<DomainEvent>();

        public void Process(DomainEvent domainEvent)
        {
            domainEvent.Process();
            _eventLog.Add(domainEvent);
        }
    }
}