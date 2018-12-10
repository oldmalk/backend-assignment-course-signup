using System.Collections.Generic;
using System.Threading.Tasks;
using MyCompany.Domain.Events;

namespace MyCompany.Application.Events
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IList<DomainEvent> _eventLog = new List<DomainEvent>();

        public async Task Process(DomainEvent domainEvent)
        {
             await Task.Run(() => domainEvent.Process());
            _eventLog.Add(domainEvent);
        }
    }
}