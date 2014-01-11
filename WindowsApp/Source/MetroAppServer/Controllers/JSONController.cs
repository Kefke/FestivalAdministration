using MetroAppServer.Models;
using MetroAppServer.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.IO;

namespace MetroAppServer.Controllers
{
    public class JSONController : Controller
    {
        //
        // GET: /JSON/

        public ActionResult Index()
        {
            List<Band> bands = BandSQLRepository.GetBands();
            return Json(bands, JsonRequestBehavior.AllowGet);
            //return View();
        }

        /*public ActionResult lineup()
        {
            List<Band> bands = BandSQLRepository.GetBandsOrderTimeSlot();

            // Convert bands to json
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Band>));
            serializer.WriteObject(stream, bands);

            // Get the stream
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);

            // Return JSON
            return new JSONResult(sr.ReadToEnd());
        }

        public ActionResult genres()
        {
            List<Band> bands = BandSQLRepository.GetBandsOrderedByGenre();

            // Convert bands to json
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Band>));
            serializer.WriteObject(stream, bands);

            // Get the stream
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);

            // Return JSON
            return new JSONResult(sr.ReadToEnd());
        }*/
    }
}
