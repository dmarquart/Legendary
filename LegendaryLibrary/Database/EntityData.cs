using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LegendaryLibrary
{
    public class EntityData
    {
        private EntityData() { }
        public EntityData(int entity_Id, string entity_Type, string location, string abbreviated_Name, 
                          string name, string fund, int sortOrder, string trialBalanceName )
        {
            Entity_Id = entity_Id;
            Entity_Type = entity_Type;
            Location = location;
            Abbreviated_Name = abbreviated_Name;
            Name = name;
            Fund = fund;
            SortOrder = sortOrder;
            Trial_Balance_Name = trialBalanceName;
        }

        public int Entity_Id { get; }
        public string Entity_Type { get; }
        public string Location { get; }
        public string Abbreviated_Name { get; }
        public string Name { get; }
        public string Fund { get; }
        public int SortOrder { get; }
        public string Trial_Balance_Name { get; }

        static public List<EntityData> GetAllEntityData()
        {
            string queryString = "SELECT Entity_Id, Entity_Type, Location, Abbreviated_Name, " +
                                 "       Name, Fund, SortOrder, Trial_Balance_Name " +
                                 "  FROM Entity ";

            var allEntityData = new List<EntityData>();

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
                        int entity_Id = reader.GetInt32(0);
                        string entity_Type = (reader[1] is DBNull) ? "" : reader.GetString(1);
                        string location = (reader[2] is DBNull) ? "" : reader.GetString(2).Trim();
                        string abbreviated_Name = (reader[3] is DBNull) ? "" : reader.GetString(3).Trim();
                        string name = (reader[4] is DBNull) ? "" : reader.GetString(4).Trim();
                        string fund = (reader[5] is DBNull) ? "" : reader.GetString(5).Trim();
                        int sortOrder = (reader[6] is DBNull) ? 99999 : reader.GetInt32(6);
                        string trialBalanceName = (reader[7] is DBNull) ? "" : reader.GetString(7).Trim();

                        var newEntityData = new EntityData(entity_Id, entity_Type, location, abbreviated_Name,
                                                           name, fund, sortOrder, trialBalanceName);

                        allEntityData.Add(newEntityData);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Legendary.UpdateStatus(ex);
                }
            }

            return allEntityData;
        }
    }
}
