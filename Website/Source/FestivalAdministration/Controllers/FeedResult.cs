using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace FestivalAdministration.Controllers
{
    public class FeedResult: ActionResult
    {
        // Force constructor with path parameter
        private FeedResult()
        {
        }

        public FeedResult(string path)
        {
            Path = path;
        }

        public string Path { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            // Change ContentType
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/rss+xml";

            // Write file to buffer
            StreamReader reader = File.OpenText(Path);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                response.Output.WriteLine(line);
            }

            // Close file
            reader.Close();
        }
    }
}