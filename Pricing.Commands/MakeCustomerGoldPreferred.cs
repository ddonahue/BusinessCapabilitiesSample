using System;
using NServiceBus;

namespace Pricing.Commands
{
    public class MakeCustomerGoldPreferred : ICommand
    {
        public Guid CustomerId { get; set; }
    }
}