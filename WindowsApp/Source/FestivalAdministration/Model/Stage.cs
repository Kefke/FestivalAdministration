using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    public class Stage : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name has to be between 3 and 50 characters")]
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
                    if (reader != null)
                        reader.Close();
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

        public static int AddStage(Stage stage)
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
                if (reader != null)
                    reader.Close();

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

        public static string GetStageByID(int id)
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

        public Stage Copy()
        {
            return new Stage() { ID = this.ID, Name = this.Name };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
