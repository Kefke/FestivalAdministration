using System.Web;
using System.Web.Mvc;
using FestivalAdministration.Filters;

namespace FestivalAdministration
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new InitializeSimpleMembershipAttribute());
		}
	}
}