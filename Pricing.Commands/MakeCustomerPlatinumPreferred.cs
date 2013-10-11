using System;
using NServiceBus;

namespace Pricing.Commands
{
    public class MakeCustomerPlatinumPreferred : ICommand
    {
        public Guid CustomerId { get; set; }
    }
}