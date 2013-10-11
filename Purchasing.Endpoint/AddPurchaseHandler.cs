using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.Transactions;
using NServiceBus;
using Purchasing.Commands;
using Purchasing.Events;

namespace Purchasing.Endpoint
{
    public class AddPurchaseHandler : IHandleMessages<AddPurchase>
    {
        private readonly IBus bus;

        public AddPurchaseHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(AddPurchase message)
        {
            const string sql = "INSERT INTO Purchases (PurchaseId, CustomerId) VALUES (@PurchaseId, @CustomerId)";

            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (var connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["purchasing"].ConnectionString))
                {
                    connection.Open();

                    var command = new SqlCeCommand(sql, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@PurchaseId", message.PurchaseId);
                    command.Parameters.AddWithValue("@CustomerId", message.CustomerId);

                    command.ExecuteNonQuery();
                }
            }
            bus.Publish(new PurchaseAdded { CustomerId = message.CustomerId });
        }
    }
}
