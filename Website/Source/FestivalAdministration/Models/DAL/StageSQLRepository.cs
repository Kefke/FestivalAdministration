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
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /*public static int AddStage(Stage stage)
        {
            // If _Stage is null, create the Observable Collection
            if (_stages == null) GetStages();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@name", stage.Name);
                DbDataReader reader = Database.GetData("INSERT INTO stage(name) VALUES(@name); SELECT LAST_INSERT_ID() AS ID;", param);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) stage.ID = -1;
                    else stage.ID = Convert.ToInt32(record["ID"]);
                }

                _stages.Add(stage);
                return stage.ID;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public static void UpdateStage(Stage stage)
        {
            // If _Stage is null, create the Observable Collection
            if (_stages == null) GetStages();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", stage.ID);
                DbParameter param2 = Database.AddParameter("@name", stage.Name);
                int affectedRows = Database.ModifyData("UPDATE stage SET name = @name WHERE id = @id", param1, param2);
                if (affectedRows == 0) return;

                // Update _stages
                _stages[GetIndexByID(stage.ID)] = stage;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteStage(Stage stage)
        {
            // If _Stage is null, create the Observable Collection
            if (_stages == null) GetStages();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", stage.ID);
                int affectedRows = Database.ModifyData("DELETE FROM stage WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _stages
                _stages.RemoveAt(GetIndexByID(stage.ID));
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetTypeByID(int id)
        {
            // If _Stage is null, create the Observable Collection
            if (_stages == null) GetStages();

            foreach (Stage stage in _stages)
            {
                if (stage.ID == id)
                    return stage.Name;
            }

            return "";
        }

        public static int GetIndexByID(int id)
        {
            // If _Stage is null, create the Observable Collection
            if (_stages == null) GetStages();

            for (int i = 0; i < _stages.Count; ++i)
            {
                if (_stages[i].ID == id)
                    return i;
            }

            return -1;
        }*/
    }
}