using System;
using NServiceBus;

namespace Pricing.Commands
{
    public class MakeCustomerStandard : ICommand
    {
        public Guid CustomerId { get; set; }
    }
}
