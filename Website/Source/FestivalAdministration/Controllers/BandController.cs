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
        public ActionResult Index()
        {
            return View("Index",BandSQLRepository.GetBands());
        }

        [AllowAnonymous]
        public ActionResult Detail(int? bandID)
        {
            if (!bandID.HasValue) return Index();
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
