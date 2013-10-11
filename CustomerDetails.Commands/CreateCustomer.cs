using System;
using NServiceBus;

namespace CustomerDetails.Commands
{
    public class CreateCustomer : ICommand
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
