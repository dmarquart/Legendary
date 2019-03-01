using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LegendaryLibrary
{
    public class LegendaryKeyValue
    {
        private LegendaryKeyValue() { }
        public LegendaryKeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }

        static public SortedList<string, LegendaryKeyValue> LoadKeyValuesIntoSortedList()
        {
            string queryString = "SELECT ValueKey, ValueValue FROM KeyValue ";

            SortedList<string, LegendaryKeyValue> keyValues = new SortedList<string, LegendaryKeyValue>();

            using (SqlConnection connection = new SqlConnection(Legendary.GetSqlConnectionString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string key = (reader[0] is DBNull) ? "" : reader.GetString(0);
                        string value = (reader[1] is DBNull) ? "" : reader.GetString(1);
                        var keyValue = new LegendaryKeyValue(key, value);
                        keyValues.Add(key, keyValue);
                    }
                    reader.Close();

                    return keyValues;
                }
                catch (Exception ex)
                {
                    return new SortedList<string, LegendaryKeyValue>();
                }
            }
        }

        static public void UpdateOneKeyValue(LegendaryKeyValue keyValue)
        {
            try
            {
                string updateString = "UPDATE KeyValue                   " +
                                      "   SET ValueValue = @valueValue   " +
                                      " WHERE ValueKey   = @valueKey     ";


                using (SqlConnection connection = new SqlConnection(Legendary.GetSqlConnectionString()))
                {
                    SqlCommand command = new SqlCommand(updateString, connection);
                    command.Parameters.AddWithValue("@valueKey", keyValue.Key);
                    command.Parameters.AddWithValue("@valueValue", keyValue.Value);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        static public void InsertOneKeyValue(LegendaryKeyValue keyValue)
        {
            try
            {
                string updateString = "INSERT INTO KeyValue (ValueKey, ValueValue) VALUES (@valueKey, @valueValue) ";
                using (SqlConnection connection = new SqlConnection(Legendary.GetSqlConnectionString()))
                {
                    SqlCommand command = new SqlCommand(updateString, connection);
                    command.Parameters.AddWithValue("@valueKey", keyValue.Key);
                    command.Parameters.AddWithValue("@valueValue", keyValue.Value);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        static public void InsertOrUpdateOneKeyValue(LegendaryKeyValue keyValue)
        {
            try
            {
                LegendaryKeyValue existingKeyValue = LegendaryKeyValue.GetOneKeyValue(keyValue.Key);
                LegendaryKeyValue keyValueToWriteToDB = new LegendaryKeyValue(keyValue.Key, keyValue.Value);

                if (existingKeyValue != null)
                    LegendaryKeyValue.UpdateOneKeyValue(keyValueToWriteToDB);
                else
                    LegendaryKeyValue.InsertOneKeyValue(keyValueToWriteToDB);

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        static public LegendaryKeyValue GetOneKeyValue(string key)
        {
            LegendaryKeyValue keyValue = null;
            try
            {
                string queryString = "SELECT ValueKey, ValueValue " +
                                     "  FROM KeyValue " +
                                     " WHERE ValueKey = @valueKey";

                using (SqlConnection connection = new SqlConnection(Legendary.GetSqlConnectionString()))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@valueKey", key);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string valueKey = (reader[0] is DBNull) ? "" : reader.GetString(0).Trim();
                        string valueValue = (reader[1] is DBNull) ? "" : reader.GetString(1).Trim();
                        keyValue = new LegendaryKeyValue(valueKey, valueValue);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                keyValue = null;
                Legendary.UpdateStatus(ex);
            }
            return keyValue;
        }

    }
}
