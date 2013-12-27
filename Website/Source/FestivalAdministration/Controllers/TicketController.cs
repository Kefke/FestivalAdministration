using FestivalAdministration.Models;
using FestivalAdministration.Models.DAL;
using MySql.Web.Security;
using MySqlSimpleMembership.Dac;
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
            // Check if Administrator
            if (User.IsInRole("Administrators")) 
                return Overview();
            else
            {
                // Get Customer Email
                string email = null;
                using (var db = MySqlSimpleMembershipDbContext.CreateContext())
			    {
				    var userProperties = db.UserProperties.SingleOrDefault(x => x.UserName == User.Identity.Name);

                    if (userProperties != null)
                    {
                        email = userProperties.Email;
                    }
                }

                if (email == null) return View();

                // Get Tickets of customer
                List<Ticket> tickets = TicketSQLRepository.GetTickets(/*"customer1@test.com"*/email);
                if (tickets == null) return View();

                // Either make new order or show current order(s)
                if (tickets.Count > 0)
                    return View("Index", tickets);
                else return Order(null, null);
            }
        }

        [Authorize]
        public ActionResult Order(int? TicketType, uint? Amount)
        {
            // Check input
            bool valid = true;
            if (!TicketType.HasValue) valid = false;
            if (!Amount.HasValue) valid = false;
            else if (Amount <= 0) valid = false;
            else ViewBag.Amount = Amount;

            if (valid)
            {
                /*// Get Customer Details
                string email = "";
                string name = "";
                using (var db = MySqlSimpleMembershipDbContext.CreateContext())
                {
                    var userProperties = db.UserProperties.SingleOrDefault(x => x.UserName == User.Identity.Name);

                    if (userProperties != null)
                    {
                        email = userProperties.Email;
                        name = userProperties.LastName + " " + userProperties.FirstName;
                    }
                }*/

                //TicketSQLRepository.AddTicket(new Ticket() { TicketHolder = /*"customer1"*/name, TicketHolderEmail = /*"customer1@test.com"*/email, TicketTypeID = TicketType.Value, Amount = (int)Amount.Value });
                //return Index();
                return Confirm(TicketType, Amount);
            }

            List<TicketType> ticketTypes = TicketTypeSQLRepository.GetTicketTypes();
            if (ticketTypes == null) return View();

            // Create combobox
            List<SelectListItem> cboTicketType = new List<SelectListItem>();
            foreach (TicketType type in ticketTypes)
            {
                SelectListItem item = new SelectListItem();
                item.Text = type.Name;
                item.Value = type.ID.ToString();

                if (TicketType != null)
                    if (TicketType == type.ID)
                        item.Selected = true;

                cboTicketType.Add(item);
            }
            ViewBag.TicketType = cboTicketType;
            return View("Order"/*new Ticket()*/);
        }

        [Authorize]
        public ActionResult Confirm(int? TicketType, uint? Amount)
        {
            // Check input
            bool valid = true;
            if (!TicketType.HasValue) valid = false;
            if (!Amount.HasValue) valid = false;
            else if (Amount <= 0) valid = false;
            
            if (!valid) return Order(TicketType, Amount);
            return View("Confirm");
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Overview()
        {
            return View("Overview",TicketSQLRepository.GetTickets());
        }

    }
}
