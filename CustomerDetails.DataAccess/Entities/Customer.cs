using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerDetails.DataAccess.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
