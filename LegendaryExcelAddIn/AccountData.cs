using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;

namespace LegendaryExcelAddIn
{
    public class AccountData
    {
        private AccountData() { }
        public AccountData(int account_Data_Id, int ledger_Account_Id, int entity_Id,
                           DateTime value_Date, decimal value, int year, int month, int day)
        {
            Account_Data_Id = account_Data_Id;
            Ledger_Account_Id = ledger_Account_Id;
            Entity_Id = entity_Id;
            Value_Date = value_Date;
            Value = value;
            Year = year;
            Month = month;
            Day = day;
        }
        public AccountData(int account_Data_Id, int ledger_Account_Id, int entity_Id,
                   DateTime value_Date, decimal value)
        {
            Account_Data_Id = account_Data_Id;
            Ledger_Account_Id = ledger_Account_Id;
            Entity_Id = entity_Id;
            Value_Date = value_Date;
            Value = value;
            Year = value_Date.Year;
            Month = value_Date.Month;
            Day = value_Date.Day;
        }

        public int Account_Data_Id { get; }
        public int Ledger_Account_Id { get; }
        public int Entity_Id { get; }
        public DateTime Value_Date { get; }
        public decimal Value { get; }
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }

        static public List<AccountData> LoadAcccountDataByDate(DateTime valueDate)
        {
            DateTime paramValue = valueDate;
            string queryString = "SELECT Account_Data_Id, Ledger_Account_Id, Entity_Id, Value_Date, Value, Year, Month, Day " +
                                 "  FROM Account_Data " +
                                 " WHERE Value_Date = @valueDate";

            List<AccountData> accountData = new List<AccountData>();

            using (SqlConnection connection = new SqlConnection(LegendaryConstants.GetSqlConnectionString()))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@valueDate", paramValue);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int account_Data_Id = reader.GetInt32(0);
                        int ledger_Account_Id = (reader[4] is DBNull) ? 0 : reader.GetInt32(1);
                        int entity_Id = (reader[4] is DBNull) ? 0 : reader.GetInt32(2);
                        DateTime value_Date = (reader[4] is DBNull) ? DateTime.MinValue : reader.GetDateTime(3);
                        decimal value = (reader[4] is DBNull) ? (decimal)0.00 : reader.GetDecimal(4);
                        int year = (reader[4] is DBNull) ? 0 : reader.GetInt32(5);
                        int month = (reader[4] is DBNull) ? 0 : reader.GetInt32(6);
                        int day = (reader[4] is DBNull) ? 0 : reader.GetInt32(7);

                        var newAccountData = new AccountData(account_Data_Id, ledger_Account_Id,
                                                             entity_Id, value_Date, value,
                                                             year, month, day);

                        accountData.Add(newAccountData);
                    }
                    reader.Close();

                    return accountData;
                }
                catch (Exception ex)
                {
                    LegendaryConstants.UpdateStatusFromException(ex);
                    return new List<AccountData>();
                }
            }
        }

        static public AccountData GetOneAccountData(int ledgerAccountId, int entityId, DateTime valueDate)
        {
            AccountData accountData = null;
            try
            {
                string queryString = "SELECT Account_Data_Id, Ledger_Account_Id, Entity_Id, Value_Date, Value, Year, Month, Day " +
                                     "  FROM Account_Data " +
                                     " WHERE Ledger_Account_Id = @ledgerAccountId AND Entity_Id = @entityId AND Value_Date = @valueDate";

                using (SqlConnection connection = new SqlConnection(LegendaryConstants.GetSqlConnectionString()))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@ledgerAccountId", ledgerAccountId);
                    command.Parameters.AddWithValue("@entityId", entityId);
                    command.Parameters.AddWithValue("@valueDate", valueDate);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int account_Data_Id = reader.GetInt32(0);
                        int ledger_Account_Id = (reader[4] is DBNull) ? 0 : reader.GetInt32(1);
                        int entity_Id = (reader[4] is DBNull) ? 0 : reader.GetInt32(2);
                        DateTime value_Date = (reader[4] is DBNull) ? DateTime.MinValue : reader.GetDateTime(3);
                        decimal value = (reader[4] is DBNull) ? (decimal)0.00 : reader.GetDecimal(4);
                        int year = (reader[4] is DBNull) ? 0 : reader.GetInt32(5);
                        int month = (reader[4] is DBNull) ? 0 : reader.GetInt32(6);
                        int day = (reader[4] is DBNull) ? 0 : reader.GetInt32(7);

                        accountData = new AccountData(account_Data_Id, ledger_Account_Id, entity_Id, value_Date, value, year, month, day);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                accountData = null;
                LegendaryConstants.UpdateStatusFromException(ex);
            }
            return accountData;
        }

        static public void UpdateOneAccountData(AccountData accountData)
        {
            try
            {
                string updateString = "UPDATE Account_Data                           " +
                                      "   SET Ledger_Account_Id = @ledgerAccountId,  " +
                                      "       Entity_Id         = @entityId,         " +
                                      "       Value_Date        = @valueDate,        " +
                                      "       Value             = @value,            " +
                                      "       Year              = @year,             " +
                                      "       Month             = @month,            " +
                                      "       Day               = @day               " +
                                      " WHERE Account_Data_Id   = @accountDataId     ";


                using (SqlConnection connection = new SqlConnection(LegendaryConstants.GetSqlConnectionString()))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(updateString, connection);
                    command.Parameters.AddWithValue("@accountDataId", accountData.Account_Data_Id);
                    command.Parameters.AddWithValue("@ledgerAccountId", accountData.Ledger_Account_Id);
                    command.Parameters.AddWithValue("@entityId", accountData.Entity_Id);
                    command.Parameters.AddWithValue("@valueDate", accountData.Value_Date);
                    command.Parameters.AddWithValue("@value", accountData.Value);
                    command.Parameters.AddWithValue("@year", accountData.Year);
                    command.Parameters.AddWithValue("@month", accountData.Month);
                    command.Parameters.AddWithValue("@day", accountData.Day);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }
        }

        static public void InsertOneAccountData(AccountData accountData)
        {
            try
            {
                string updateString = "INSERT INTO Account_Data (Ledger_Account_Id, Entity_Id, Value_Date, Value, Year, Month, Day)     " +
                                      "                  VALUES (@ledgerAccountId, @entityId, @valueDate, @value, @year,  @month, @day) ";

                using (SqlConnection connection = new SqlConnection(LegendaryConstants.GetSqlConnectionString()))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(updateString, connection);
                    command.Parameters.AddWithValue("@ledgerAccountId", accountData.Ledger_Account_Id);
                    command.Parameters.AddWithValue("@entityId", accountData.Entity_Id);
                    command.Parameters.AddWithValue("@valueDate", accountData.Value_Date);
                    command.Parameters.AddWithValue("@value", accountData.Value);
                    command.Parameters.AddWithValue("@year", accountData.Year);
                    command.Parameters.AddWithValue("@month", accountData.Month);
                    command.Parameters.AddWithValue("@day", accountData.Day);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }
        }

        static public void InsertOrUpdateOneAccountData(AccountData accountData)
        {
            try
            {
                int accountDataId = 0;

                AccountData existingAccountData = AccountData.GetOneAccountData(accountData.Ledger_Account_Id, accountData.Entity_Id, accountData.Value_Date);

                if (existingAccountData != null)
                    accountDataId = existingAccountData.Account_Data_Id;

                AccountData accountDataToWriteToDB = new AccountData(accountDataId, accountData.Ledger_Account_Id,
                                                                  accountData.Entity_Id, accountData.Value_Date, accountData.Value,
                                                                  accountData.Year, accountData.Month, accountData.Day);

                if (accountDataId > 0)
                    AccountData.UpdateOneAccountData(accountDataToWriteToDB);
                else
                    AccountData.InsertOneAccountData(accountDataToWriteToDB);

            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }
        }

        static public void InsertAccountData(List<AccountData> accountDataList)
        {
            try
            {
                string updateString = "INSERT INTO Account_Data (Ledger_Account_Id, Entity_Id, Value_Date, Value, Year, Month, Day, Modified_DateTime)     " +
                      "                  VALUES (@ledgerAccountId, @entityId, @valueDate, @value, @year,  @month, @day, @modifiedDateTime) ";

                DateTime nowDateTime = DateTime.Now;

                using (SqlConnection connection = new SqlConnection(LegendaryConstants.GetSqlConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(updateString, connection);
                    command.Parameters.AddWithValue("@ledgerAccountId", 0);
                    command.Parameters.AddWithValue("@entityId", 0);
                    command.Parameters.AddWithValue("@valueDate", DateTime.MinValue);
                    command.Parameters.AddWithValue("@value", decimal.MinValue);
                    command.Parameters.AddWithValue("@year", DateTime.MinValue.Year);
                    command.Parameters.AddWithValue("@month", DateTime.MinValue.Month);
                    command.Parameters.AddWithValue("@day", DateTime.MinValue.Day);
                    command.Parameters.AddWithValue("@modifiedDateTime", nowDateTime);

                    foreach (var accountData in accountDataList)
                    {
                        command.Parameters["@ledgerAccountId"].Value = accountData.Ledger_Account_Id;
                        command.Parameters["@entityId"].Value = accountData.Entity_Id;
                        command.Parameters["@valueDate"].Value = accountData.Value_Date;
                        command.Parameters["@value"].Value = accountData.Value;
                        command.Parameters["@year"].Value = accountData.Year;
                        command.Parameters["@month"].Value = accountData.Month;
                        command.Parameters["@day"].Value = accountData.Day;

                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }
        }

        static public void DeleteByEntityIdValueDate(int entityId, DateTime valueDate)
        {
            try
            {
                string updateString = "DELETE FROM Account_Data " +
                                      " WHERE Entity_Id = @entityId AND Value_Date = @valueDate ";

                using (SqlConnection connection = new SqlConnection(LegendaryConstants.GetSqlConnectionString()))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(updateString, connection);
                    command.Parameters.AddWithValue("@entityId", entityId);
                    command.Parameters.AddWithValue("@valueDate", valueDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LegendaryConstants.UpdateStatusFromException(ex);
            }
        }
    }
}
