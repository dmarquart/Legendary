using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LegendaryLibrary
{
    public class ConsolidatedAccount
    {
        private ConsolidatedAccount() { }
        public ConsolidatedAccount(int consolidated_Account_Id, string number, string name, string description, 
                                   string consolidated_Type, string category, int sortOrder, int group)
        {
            Consolidated_Account_Id = consolidated_Account_Id;
            Number = number; 
            Name = name;
            Description = description;
            Consolidated_Type = consolidated_Type;
            Category = category;
            SortOrder = sortOrder;
            Group = group;
        }

        public int Consolidated_Account_Id { get; }
        public string Number { get; }
        public string Name { get; }
        public string Description { get; }
        public string Consolidated_Type { get; }
        public string Category { get; }
        public int SortOrder { get; }
        public int Group { get; }

        static public List<ConsolidatedAccount> GetByConsolidatedType(string consolidatedType, List<ConsolidatedAccount> consolidateAccounts)
        {
            var returnAccounts = new List<ConsolidatedAccount>();
            foreach (var account in consolidateAccounts)
                if (string.Compare(account.Consolidated_Type, consolidatedType, true) == 0)
                    returnAccounts.Add(account);
            return returnAccounts;
        }

        static public SortedList<string, SortedList<string, ConsolidatedAccount>> GetSortedListsByCategory(List<ConsolidatedAccount> consolidatedAccounts)
        {
            var accountListsByCategory = new SortedList<string, SortedList<string, ConsolidatedAccount>>();
            foreach (var account in consolidatedAccounts)
            {
                string accountListKey = account.SortOrder.ToString().Substring(0, 1) + account.Category;
                if (!accountListsByCategory.ContainsKey(accountListKey))
                    accountListsByCategory.Add(accountListKey, new SortedList<string, ConsolidatedAccount>());
                string accountKey = account.SortOrder.ToString("00000") + account.Name;
                accountListsByCategory[accountListKey].Add(accountKey, account);
            }
            return accountListsByCategory;
        }

        static public ConsolidatedAccount GetConsolidatedAccountById(int consolidatedAccountId, List<ConsolidatedAccount> consolidatedAccounts)
        {
            ConsolidatedAccount consolidatedAccount = null;
            foreach (var account in consolidatedAccounts)
                if (account.Consolidated_Account_Id == consolidatedAccountId)
                {
                    consolidatedAccount = account;
                    break;
                }
            return consolidatedAccount;
        }

        static public List<ConsolidatedAccount> GetAllConsolidatedAccounts()
        {
            string queryString = "SELECT Consolidated_Account_Id, Number, Name, " +
                                 "       Description, Consolidated_Type, Category, SortOrder, \"Group\" " +
                                 "  FROM Consolidated_Account ";

            List<ConsolidatedAccount> allConsolidatedAccounts = new List<ConsolidatedAccount>();

            using (SqlConnection connection = new SqlConnection(Legendary.GetSqlConnectionString()))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int consolidated_Account_Id = reader.GetInt32(0);
                        string number = (reader[1] is DBNull) ? "" : reader.GetString(1).Trim();
                        string name = (reader[2] is DBNull) ? "" : reader.GetString(2).Trim();
                        string description = (reader[3] is DBNull) ? "" : reader.GetString(3).Trim();
                        string consolidated_Type = (reader[4] is DBNull) ? "" : reader.GetString(4).Trim();
                        string category = (reader[5] is DBNull) ? "" : reader.GetString(5).Trim();
                        int sortOrder = (reader[6] is DBNull) ? 99999 : reader.GetInt32(6);
                        int group = (reader[7] is DBNull) ? 99 : reader.GetInt32(7);

                        var newConsolidatedAccount = new ConsolidatedAccount(consolidated_Account_Id, number, name, description,
                                                                             consolidated_Type, category, sortOrder, group);

                        allConsolidatedAccounts.Add(newConsolidatedAccount);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Legendary.UpdateStatus(ex);
                }
            }

            return allConsolidatedAccounts;
        }
    }

}
