using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;


namespace FestivalAdministration.Models.DAL
{
    public class StageSQLRepository
    {
        public static List<Stage> GetStages()
        {
            List<Stage> result = new List<Stage>();
            try
            {
                // Get data
                DbDataReader reader = Database.GetData("SELECT * FROM stage");
                foreach (DbDataRecord record in reader)
                {
                    // Create new Stage
                    Stage stage = new Stage();

                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) stage.ID = -1;
                    else stage.ID = Convert.ToInt32(record["ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["Name"])) stage.Name = "";
                    else stage.Name = record["Name"].ToString();

                    result.Add(stage);
                }
                reader.Close();
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static Stage GetStage(int ID)
        {
            Stage stage = new Stage();
            try
            {
                // Get data
                DbParameter param = Database.AddParameter("@id", ID);
                DbDataReader reader = Database.GetData("SELECT * FROM stage WHERE ID = @id", param);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) stage.ID = -1;
                    else stage.ID = Convert.ToInt32(record["ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["Name"])) stage.Name = "";
                    else stage.Name = record["Name"].ToString();
                }
                reader.Close();
                return stage;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}