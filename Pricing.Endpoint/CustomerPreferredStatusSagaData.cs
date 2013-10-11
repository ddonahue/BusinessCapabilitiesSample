using System;
using NServiceBus.Saga;

namespace Pricing.Endpoint
{
    public class CustomerPreferredStatusSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public Guid CustomerId { get; set; }
        public int NumberOfPurchases { get; set; }
    }
}