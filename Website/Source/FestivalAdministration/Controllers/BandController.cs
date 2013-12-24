﻿using FestivalAdministration.Models.DAL;
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
            return View(BandSQLRepository.GetBands());
        }

        [AllowAnonymous]
        public ActionResult Detail(int bandID)
        {
            return View(BandSQLRepository.GetBand(bandID));
        }

    }
}