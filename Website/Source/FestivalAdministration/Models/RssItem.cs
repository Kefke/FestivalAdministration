using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models
{
    public class RssItem
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _link;

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        private int _guid;

        public int GuID
        {
            get { return _guid; }
            set { _guid = value; }
        }

        private DateTime _pubDate;

        public DateTime PubDate
        {
            get { return _pubDate; }
            set { _pubDate = value; }
        }
    }
}