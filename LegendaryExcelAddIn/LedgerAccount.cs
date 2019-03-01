using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LegendaryExcelAddIn
{
    public class LedgerAccount
    {
        private LedgerAccount() { }
        public LedgerAccount(int ledger_Account_Id, int ledger_Id, int consolidated_Account_Id,
                             string number, string name, string description, string entity_Type)
        {
            Ledger_Account_Id = ledger_Account_Id;
            Ledger_Id = ledger_Id;
            Consolidated_Account_Id = consolidated_Account_Id;
            Number = number;
            Name = name;
            Description = description;
            Entity_Type = entity_Type;
        }

        public int Ledger_Account_Id { get; }
        public int Ledger_Id { get; }
        public int Consolidated_Account_Id { get; }
        public string Number { get; }
        public string Name { get; }
        public string Description { get; }
        public string Entity_Type { get; }

        static public LedgerAccount GetLedgerAccountById(int ledgerAccountId, List<LedgerAccount> ledgerAccounts)
        {
            LedgerAccount ledgerAccount = null;
            foreach (var account in ledgerAccounts)
                if (account.Ledger_Account_Id == ledgerAccountId)
                {
                    ledgerAccount = account;
                    break;
                }
            return ledgerAccount;
        }

        static public LedgerAccount GetLedgerAccountByAccountNumber(string accountNumber, List<LedgerAccount> ledgerAccounts)
        {
            LedgerAccount ledgerAccount = null;
            foreach (var account in ledgerAccounts)
            {
                if (string.Compare(account.Number, accountNumber, true) == 0)
                {
                    ledgerAccount = account;
                    break;
                }
                double ledgerAccountNumberAsDouble;
                double accountNumberAsDouble;
                bool parseStatus = true;
                parseStatus &= double.TryParse(account.Number, out ledgerAccountNumberAsDouble);
                parseStatus &= double.TryParse(accountNumber, out accountNumberAsDouble);
                if (parseStatus && (ledgerAccountNumberAsDouble == accountNumberAsDouble))
                {
                    ledgerAccount = account;
                    break;
                }
            }
            return ledgerAccount;
        }

        static public LedgerAccount GetLedgerAccountByAccountName(string accountName, List<LedgerAccount> ledgerAccounts)
        {
            LedgerAccount ledgerAccount = null;
            foreach (var account in ledgerAccounts)
            {
                if (string.Compare(account.Name, accountName, true) == 0)
                {
                    ledgerAccount = account;
                    break;
                }
            }
            return ledgerAccount;
        }

        static public List<LedgerAccount> GetAllLedgerAccounts()
        {
            string queryString = "SELECT Ledger_Account_Id, Ledger_Id, Consolidated_Account_Id, " +
                                 "       Number, Name, Description, Entity_Type " +
                                 "  FROM Ledger_Account ";

            List<LedgerAccount> allLedgerAccounts = new List<LedgerAccount>();

            using (SqlConnection connection = new SqlConnection(LegendaryConstants.GetSqlConnectionString()))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int ledger_Account_Id = reader.GetInt32(0);
                        int ledger_Id = (reader[1] is DBNull) ? 0 : reader.GetInt32(1);
                        int consolidated_Account_Id = (reader[2] is DBNull) ? 0 : reader.GetInt32(2);
                        string number = (reader[3] is DBNull) ? "" : reader.GetString(3).Trim();
                        string name = (reader[4] is DBNull) ? "" : reader.GetString(4).Trim();
                        string description = (reader[5] is DBNull) ? "" : reader.GetString(5).Trim();
                        string entity_Type = (reader[6] is DBNull) ? "" : reader.GetString(6).Trim();

                        var newLedgerAccount = new LedgerAccount(ledger_Account_Id, ledger_Id, consolidated_Account_Id,
                                                                 number, name, description, entity_Type);

                        allLedgerAccounts.Add(newLedgerAccount);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    LegendaryConstants.UpdateStatusFromException(ex);
                }
            }

            return allLedgerAccounts;
        }
    }
}
