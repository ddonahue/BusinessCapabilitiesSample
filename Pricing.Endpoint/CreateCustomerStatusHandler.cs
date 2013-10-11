using System;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.Transactions;
using NServiceBus;
using Pricing.Commands;

namespace Pricing.Endpoint
{
    public class CreateCustomerStatusHandler : IHandleMessages<MakeCustomerStandard>, IHandleMessages<MakeCustomerGoldPreferred>, IHandleMessages<MakeCustomerPlatinumPreferred>
    {
        public void Handle(MakeCustomerStandard message)
        {
            CreateCustomerStatusRecordIfDoesNotExist(message.CustomerId);
        }

        public void Handle(MakeCustomerGoldPreferred message)
        {
            CreateCustomerStatusRecordIfDoesNotExist(message.CustomerId);

            string sql = "UPDATE customerStatus SET Status = @Status WHERE CustomerId = @CustomerId";
            RunQuery(sql, message.CustomerId, "Gold");
        }

        public void Handle(MakeCustomerPlatinumPreferred message)
        {
            CreateCustomerStatusRecordIfDoesNotExist(message.CustomerId);

            const string sql = "UPDATE customerStatus SET Status = @Status WHERE CustomerId = @CustomerId";
            RunQuery(sql, message.CustomerId, "Platinum");
        }

        private void CreateCustomerStatusRecordIfDoesNotExist(Guid customerId)
        {
            if (!CustomerStatusRecordDoesNotExist(customerId)) return;

            var sql = "INSERT INTO customerStatus (CustomerId, Status) VALUES (@CustomerId, @Status)";
            RunQuery(sql, customerId, "Standard");
        }

        private static bool CustomerStatusRecordDoesNotExist(Guid customerId)
        {
            const string sql = "SELECT COUNT(CustomerId) FROM customerStatus WHERE CustomerId = @CustomerId";
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (var connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["pricing"].ConnectionString))
                {
                    connection.Open();

                    var command = new SqlCeCommand(sql, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    var result = (int)command.ExecuteScalar();
                    return result <= 0;
                }
            }
        }

        private void RunQuery(string sql, Guid customerId, string preferredStatus)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (var connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["pricing"].ConnectionString))
                {
                    connection.Open();

                    var command = new SqlCeCommand(sql, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@Status", preferredStatus);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
