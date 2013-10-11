using CustomerDetails.Events;
using NServiceBus.Saga;
using Pricing.Commands;
using Purchasing.Events;

namespace Pricing.Endpoint
{
    public class CustomerPreferredStatusSaga : Saga<CustomerPreferredStatusSagaData>, IAmStartedByMessages<CustomerCreated>, IAmStartedByMessages<PurchaseAdded>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<CustomerCreated>(msg => msg.CustomerId).ToSaga(sagaData => sagaData.CustomerId);
            ConfigureMapping<PurchaseAdded>(msg => msg.CustomerId).ToSaga(sagaData => sagaData.CustomerId);
        }

        public void Handle(CustomerCreated message)
        {
            Data.CustomerId = message.CustomerId;
            Bus.SendLocal(new MakeCustomerStandard { CustomerId = message.CustomerId});
        }

        public void Handle(PurchaseAdded message)
        {
            Data.CustomerId = message.CustomerId; // do this because this message COULD arrive first!
            Data.NumberOfPurchases = Data.NumberOfPurchases + 1;

            if (Data.NumberOfPurchases >= 10)
            {
                Bus.SendLocal(new MakeCustomerPlatinumPreferred {CustomerId = message.CustomerId});
            }
            else if (Data.NumberOfPurchases >= 5)
            {
                Bus.SendLocal(new MakeCustomerGoldPreferred {CustomerId = message.CustomerId});
            }
        }
    }
}