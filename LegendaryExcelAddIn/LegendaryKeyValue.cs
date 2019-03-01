using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LegendaryExcelAddIn
{
    class LegendaryKeyValue
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

            using (SqlConnection connection = new SqlConnection(LegendaryConstants.GetSqlConnectionString()))
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

    }
}
