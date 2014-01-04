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

        public PartialViewResult News()
        {
            RssChannel rss = RssRepository.GetRssFeed(Server.MapPath("~/Content/feed.rss"));
            if (rss != null)
            {
                // Return a random News message
                if (rss.Items.Count > 0)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, rss.Items.Count);
                    return PartialView("_NewsPartial", rss.Items[randomNumber]);
                }
            }
            // Return a empty News message
            return PartialView("_NewsPartial", null);
        }

        public ActionResult RSS()
        {
            // Check if Administrator
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrators"))
            {
                return RSSOverview();
            }

            // Else try to return the feed
            RssChannel rss = RssRepository.GetRssFeed(Server.MapPath("~/Content/feed.rss"));
            if (rss == null) return RSSError();
            // Return the feed
            return new FeedResult(Server.MapPath("~/Content/feed.rss"));
        }

        public ActionResult RSSError()
        {
            return View("RSSError");
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult RSSOverview()
        {
            RssChannel rss = RssRepository.GetRssFeed(Server.MapPath("~/Content/feed.rss"));
            if (rss == null) return CreateRSS();
            ViewBag.RSS = rss;
            return View("RSSOverview");
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult CreateRSS()
        {
            RssChannel rss = RssRepository.GetRssFeed(Server.MapPath("~/Content/feed.rss"));
            ViewBag.RSS = rss;

            // If RSS exists and the titles are not all filled, fill with the fields with the existing values
            if (rss != null && !(ViewBag.ITitle == "" || ViewBag.Description == "" || ViewBag.Link == ""))
            {
                ViewBag.ITitle = rss.Title;
                ViewBag.Description = rss.Description;
                ViewBag.Link = rss.Link;
            }

            return View("CreateRSS");
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult SaveRSS(string ITitle, string Description, string Link)
        {
            // Check if valid
            if (ITitle == "" || Description == "" || Link == "")
            {
                ViewBag.ITitle = ITitle;
                ViewBag.Description = Description;
                ViewBag.Link = Link;
                return CreateRSS();
            }

            // Create new / updated channel
            RssChannel rss = new RssChannel();

            rss.Title = ITitle;
            rss.Description = Description;
            rss.Link = Link;
            rss.LastBuild = DateTime.Now;
            rss.PubDate = rss.LastBuild;
            rss.Ttl = 1800;

            RssRepository.EditChannel(rss);

            return RSSOverview();
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult CreateRSSItem()
        {
            return View("CreateRSSItem");
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult SaveRSSItem(string ITitle, string Description, string Link)
        {
            // Check if valid
            if (ITitle == "" || Description == "" || Link == "")
            {
                ViewBag.ITitle = ITitle;
                ViewBag.Description = Description;
                ViewBag.Link = Link;
                return CreateRSSItem();
            }

            RssChannel rss = RssRepository.GetRssFeed(Server.MapPath("~/Content/feed.rss"));

            // Create new Item
            RssItem item = new RssItem();

            item.Title = ITitle;
            item.Description = Description;
            item.Link = Link;
            item.PubDate = DateTime.Now;
            if (rss.Items.Count > 0) item.GuID = rss.Items.Last().GuID + 1;
            else item.GuID = 1;

            RssRepository.AddItem(item);

            return RSSOverview();
        }
	}
}
