using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MetroAppServer.Controllers
{
    public class JSONResult: ActionResult
    {
        // Force constructor with data parameter
        private JSONResult()
        {
        }

        public JSONResult(string data)
        {
            Data = data;
        }

        public string Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            // Change ContentType
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";

            // Write Data to buffer
            response.Output.Write(Data);
        }
    }
}