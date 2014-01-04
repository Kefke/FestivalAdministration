using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models.DAL
{
    public class FestivalSQLRepository
    {
        public static Festival GetFestival()
        {
            try
            {
                // Get data
                DbDataReader reader = Database.GetData("SELECT * FROM festival");
                foreach (DbDataRecord record in reader)
                {
                    // Create new Festival
                    Festival festival = new Festival();

                    // Get ID
                    if (DBNull.Value.Equals(record["ID"])) festival.ID = -1;
                    else festival.ID = Convert.ToInt32(record["ID"]);

                    // Get Name
                    if (DBNull.Value.Equals(record["Name"])) festival.Name = "";
                    else festival.Name = record["Name"].ToString();

                    // Get Street
                    if (DBNull.Value.Equals(record["Street"])) festival.Street = "";
                    else festival.Street = record["Street"].ToString();

                    // Get City
                    if (DBNull.Value.Equals(record["City"])) festival.City = "";
                    else festival.City = record["City"].ToString();

                    reader.Close();
                    return festival;
                }
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