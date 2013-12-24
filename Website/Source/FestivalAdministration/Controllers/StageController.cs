using FestivalAdministration.Models;
using FestivalAdministration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestivalAdministration.Controllers
{
    public class StageController : Controller
    {
        //
        // GET: /Stage/

        public ActionResult Index()
        {
            return View(StageSQLRepository.GetStages());
        }

    }
}
