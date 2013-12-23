using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MySql.Web.Security;
using MySql.Data.MySqlClient;

namespace MySqlSimpleMembership.Dac
{
	public class MySqlSimpleMembershipDbContext : MySqlSecurityDbContext
	{
		// public non argument constructor for MySqlSimpleMembershipProvider
		public MySqlSimpleMembershipDbContext()
			: base("SimpleMembershipConnectionString")
		{
		}

		public static MySqlSimpleMembershipDbContext CreateContext()
		{
			return new MySqlSimpleMembershipDbContext();
		}

		public DbSet<UserProperty> UserProperties
		{
			get;
			set;
		}
	}
}
