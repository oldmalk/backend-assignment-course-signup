using System;

namespace MyCompany.Domain
{
    public abstract class DomainEvent
    {
        public DomainEvent(DateTime occurred)
        {
            Occurred = occurred;
            Recorded = DateTime.Now;
        }

        public DateTime Occurred { get; }
        public DateTime Recorded { get; }

        public abstract void Process();
    }
}