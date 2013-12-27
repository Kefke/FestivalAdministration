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
            /*// Check input
            bool valid = true;
            if (!TicketType.HasValue) valid = false;
            if (!Amount.HasValue) valid = false;
            else if (Amount <= 0) valid = false;
            else ViewBag.Amount = Amount;
            if (ViewBag.Left != null) valid = false;

            /*if (valid)
            {*/
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
                /*return Confirm(TicketType, Amount);
            }*/

            if (Amount.HasValue) 
                if (Amount > 0) 
                    ViewBag.Amount = Amount;

            List<TicketType> ticketTypes = TicketTypeSQLRepository.GetTicketTypes();
            if (ticketTypes == null) return View();

            // Create combobox
            List<SelectListItem> cboTicketType = new List<SelectListItem>();
            foreach (TicketType type in ticketTypes)
            {
                SelectListItem item = new SelectListItem();
                item.Text = type.Name;
                item.Value = type.ID.ToString();

                if (TicketType.HasValue)
                    if (TicketType.Value == type.ID)
                        item.Selected = true;

                cboTicketType.Add(item);
            }
            ViewBag.TicketType = cboTicketType;
            return View("Order"/*new Ticket()*/);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Confirm(int? TicketType, uint? Amount)
        {
            // Check input
            bool valid = true;
            if (!TicketType.HasValue) valid = false;
            if (!Amount.HasValue) valid = false;
            else if (Amount <= 0) valid = false;
            
            if (!valid) return Order(TicketType, Amount);

            TicketType tickettype = TicketTypeSQLRepository.GetTicketType(TicketType.Value);
            if (tickettype == null) return Order(null, Amount);

            int ticketsLeft = tickettype.TicketsLeft;/*tickettype.AvailableTickets - TicketSQLRepository.GetNumberOfTicketsByType(TicketType.Value);*/
            if (Amount > ticketsLeft)
            {
                ViewBag.left = ticketsLeft;
                return Order(TicketType, Amount);
            }

            // Fill ViewBag with info
            using (var db = MySqlSimpleMembershipDbContext.CreateContext())
            {
                var userProperties = db.UserProperties.SingleOrDefault(x => x.UserName == User.Identity.Name);

                if (userProperties != null)
                {
                    ViewBag.Name = userProperties.LastName + " " + userProperties.FirstName;
                    ViewBag.Email = userProperties.Email;
                }
            }

            ViewBag.Type = tickettype;
            ViewBag.Amount = Amount.Value;

            return View("Confirm");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(int? TicketType, uint? Amount)
        {
            // Check input
            bool valid = true;
            if (!TicketType.HasValue) valid = false;
            if (!Amount.HasValue) valid = false;
            else if (Amount <= 0) valid = false;

            if (!valid) return Order(TicketType, Amount);

            TicketType tickettype = TicketTypeSQLRepository.GetTicketType(TicketType.Value);
            if (tickettype == null) return Order(null, Amount);

            int ticketsLeft = tickettype.TicketsLeft;/*tickettype.AvailableTickets - TicketSQLRepository.GetNumberOfTicketsByType(TicketType.Value);*/
            if (Amount > ticketsLeft)
            {
                ViewBag.left = ticketsLeft;
                return Order(TicketType, Amount);
            }

            // Reserve ticket in database
            TicketTypeSQLRepository.UpdateTicketTypeLeft(tickettype.ID, ticketsLeft-(int)Amount);
            tickettype = TicketTypeSQLRepository.GetTicketType(tickettype.ID);
            // Overbooked
            if (tickettype.TicketsLeft < 0)
            {
                TicketTypeSQLRepository.UpdateTicketTypeLeft(tickettype.ID, tickettype.TicketsLeft + (int)Amount);
                ViewBag.left = ticketsLeft;
                return Order(TicketType, Amount);
            }

            // Get data to create the ticket
            string name = "";
            string email = "";
            using (var db = MySqlSimpleMembershipDbContext.CreateContext())
            {
                var userProperties = db.UserProperties.SingleOrDefault(x => x.UserName == User.Identity.Name);

                if (userProperties != null)
                {
                    name = userProperties.LastName + " " + userProperties.FirstName;
                    email = userProperties.Email;
                }
            }

            // Add the ticket
            TicketSQLRepository.AddTicket(new Ticket() { TicketHolder = name, TicketHolderEmail = email, TicketTypeID = tickettype.ID, Amount = (int)Amount });

            return Index();
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Overview()
        {
            return View("Overview",TicketSQLRepository.GetTickets());
        }

    }
}
