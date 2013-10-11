using System;
using NServiceBus;

namespace CustomerDetails.Events
{
    public class CustomerCreated : IEvent
    {
        public Guid CustomerId { get; set; }
    }
}
