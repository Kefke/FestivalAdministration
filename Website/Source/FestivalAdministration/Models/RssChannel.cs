using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalAdministration.Models
{
    public class RssChannel
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

        private DateTime _lastBuild;

        public DateTime LastBuild
        {
            get { return _lastBuild; }
            set { _lastBuild = value; }
        }

        private DateTime _pubDate;

        public DateTime PubDate
        {
            get { return _pubDate; }
            set { _pubDate = value; }
        }

        private int _ttl;

        public int Ttl
        {
            get { return _ttl; }
            set { _ttl = value; }
        }

        private List<RssItem> _items;

        public List<RssItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }
        
    }
}