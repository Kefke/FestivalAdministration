using FestivalAdministration.Models;
using FestivalAdministration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestivalAdministration.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Error()
		{
			return View("Error");
		}

        public ActionResult Location()
        {
            Festival festival = FestivalSQLRepository.GetFestival();
            return View("Location", festival);
        }
	}
}
