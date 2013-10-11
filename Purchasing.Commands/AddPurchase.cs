using System;
using NServiceBus;

namespace Purchasing.Commands
{
    public class AddPurchase : ICommand
    {
        public Guid PurchaseId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
