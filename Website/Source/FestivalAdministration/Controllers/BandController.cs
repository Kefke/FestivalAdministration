using FestivalAdministration.Models;
using FestivalAdministration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestivalAdministration.Controllers
{
    public class BandController : Controller
    {
        //
        // GET: /Band/

        [AllowAnonymous]
        public ActionResult Index(string Band, string Stage, string TimeSlot)
        {
            if (Band != null)
            {
                if (Band.ToLower() == "asc")
                {
                    ViewBag.BandAsc = true;
                    return View("Index", BandSQLRepository.GetBandsOrderName(true));
                }
                if (Band.ToLower() == "desc")
                {
                    ViewBag.BandAsc = false;
                    return View("Index", BandSQLRepository.GetBandsOrderName(false));
                }
            }

            if (Stage != null)
            {
                if (Stage.ToLower() == "asc")
                {
                    ViewBag.StageAsc = true;
                    return View("Index", BandSQLRepository.GetBandsOrderTimeSlot(true,true));
                }
                if (Stage.ToLower() == "desc")
                {
                    ViewBag.StageAsc = false;
                    return View("Index", BandSQLRepository.GetBandsOrderTimeSlot(true,false));
                }
            }

            if (TimeSlot != null)
            {
                if (TimeSlot.ToLower() == "asc")
                {
                    ViewBag.TimeSlotAsc = true;
                    return View("Index", BandSQLRepository.GetBandsOrderTimeSlot(false,true));
                }
                if (TimeSlot.ToLower() == "desc")
                {
                    ViewBag.TimeSlotAsc = false;
                    return View("Index", BandSQLRepository.GetBandsOrderTimeSlot(false,false));
                }
            }
            ViewBag.BandAsc = true;
            return View("Index", BandSQLRepository.GetBandsOrderName(true));
        }

        [AllowAnonymous]
        public ActionResult Detail(int? bandID)
        {
            if (!bandID.HasValue) return Index(null, null, null);
            Band band = BandSQLRepository.GetBand(bandID.Value);
            if (band == null) return Error(bandID.Value);
            return View("Detail",band);
        }

        [AllowAnonymous]
        public ActionResult Error(int bandID)
        {
            ViewBag.BandID = bandID;
            return View("Error");
        }
    }
}
