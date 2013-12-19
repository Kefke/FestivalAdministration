using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class Stage
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private static ObservableCollection<Stage> _stages = null;

        public static ObservableCollection<Stage> GetStages()
        {
            // If _stages is null, create the Observable Collection
            if (_stages == null)
            {
                try
                {
                    // Create _stages
                    _stages = new ObservableCollection<Stage>();

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

                        // Add Stage
                        _stages.Add(stage);
                    }
                }

                // Fail
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Clear Stages
                    _stages.Clear();
                    _stages = null;
                }

            }

            // Return _stages
            return _stages;
        }

        public static void AddStage(string name)
        {
            // If _Stage is null, create the Observable Collection
            if (_stages == null) GetStages();

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@name", name);
                int affectedRows = Database.ModifyData("INSERT INTO stage(name) VALUES(@name)", param);
                if (affectedRows == 0) return;

                // Get ID from db
                int id = 0;
                DbDataReader reader = Database.GetData("SELECT ID FROM stage WHERE name = @name", param);
                foreach (DbDataRecord record in reader)
                {
                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) id = -1;
                    else id = Convert.ToInt32(record["ID"]);
                }

                _stages.Add(new Stage() { ID = id, Name = name });
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateStage(int index, string newname)
        {
            // If _Stage is null, create the Observable Collection
            if (_stages == null) GetStages();

            try
            {
                // Update db
                DbParameter param1 = Database.AddParameter("@id", _stages[index].ID);
                DbParameter param2 = Database.AddParameter("@name", newname);
                int affectedRows = Database.ModifyData("UPDATE stage SET name = @name WHERE id = @id", param1, param2);
                if (affectedRows == 0) return;

                // Update _stages
                _stages[index].Name = newname;
            }

            // Fail
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteStage(int index)
        {
            // If _Stage is null, create the Observable Collection
            if (_stages == null) GetStages();

            // Only execute if index is valid
            if (_stages.Count <= index) return;

            try
            {
                // Add to db
                DbParameter param = Database.AddParameter("@id", _stages[index].ID);
                int affectedRows = Database.ModifyData("DELETE FROM stage WHERE id = @id", param);
                if (affectedRows == 0) return;

                // Update _stages
                _stages.RemoveAt(index);
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
        }

    }
}
