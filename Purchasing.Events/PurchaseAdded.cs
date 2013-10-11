using System;
using NServiceBus;

namespace Purchasing.Events
{
    public class PurchaseAdded : IEvent
    {
        public Guid CustomerId { get; set; }
    }
}
