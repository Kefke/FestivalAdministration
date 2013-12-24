using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Web.Security;

namespace MySqlSimpleMembership.Dac
{
	public class DropCreateDatabaseIfModelChangesInitializer : DropCreateMySqlDatabaseIfModelChanges<MySqlSimpleMembershipDbContext>
	{
		protected override void Seed(MySqlSimpleMembershipDbContext db)
		{
			/*db.UserProperties.Add(new UserProperty
			{
				UserId = 1,
				UserName = "admin",
				Email = "xyz3710@gmail.com",
				LastName = "Kim",
				FirstName = "Ki Won"
			});*/
		}
	}
}
