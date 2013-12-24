using FestivalAdministration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestivalAdministration.Controllers
{
    public class TicketController : Controller
    {
        //
        // GET: /Ticket/
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Administrators")) return Overview();
            else return View(TicketSQLRepository.GetTickets("customer1@test.com"));
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Overview()
        {
            return View(TicketSQLRepository.GetTickets());
        }

    }
}
