using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace FestivalAdministration.Models.DAL
{
    public class RssRepository
    {
        private static string Path = "";

        private static RssChannel _channel = null;

        public static RssChannel GetRssFeed(string path)
        {
            Path = path;
            if (_channel == null)
            {
                _channel = new RssChannel();
                _channel.Items = new List<RssItem>();

                try
                {
                    // Parse RSS
                    using (XmlReader reader = XmlReader.Create(path))
                    {
                        // Get channel Info
                        if (!reader.ReadToFollowing("channel")) return null;

                        if (reader.ReadToFollowing("title"))
                        {
                            _channel.Title = reader.ReadElementContentAsString();
                        }
                        if (reader.ReadToFollowing("description"))
                        {
                            _channel.Description = reader.ReadElementContentAsString();
                        }
                        if (reader.ReadToFollowing("link"))
                        {
                            _channel.Link = reader.ReadElementContentAsString();
                        }
                        if (reader.ReadToFollowing("lastBuildDate"))
                        {
                            _channel.LastBuild = StringToDateTime(reader.ReadElementContentAsString());
                        }
                        if (reader.ReadToFollowing("pubDate"))
                        {
                            _channel.PubDate = StringToDateTime(reader.ReadElementContentAsString());
                        }
                        if (reader.ReadToFollowing("ttl"))
                        {
                            _channel.Ttl = reader.ReadElementContentAsInt();
                        }

                        // Get items
                        while (reader.ReadToFollowing("item"))
                        {
                            RssItem item = new RssItem();

                            if (reader.ReadToFollowing("title"))
                            {
                                item.Title = reader.ReadElementContentAsString();
                            }
                            if (reader.ReadToFollowing("description"))
                            {
                                item.Description = reader.ReadElementContentAsString();
                            }
                            if (reader.ReadToFollowing("link"))
                            {
                                item.Link = reader.ReadElementContentAsString();
                            }
                            if (reader.ReadToFollowing("guid"))
                            {
                                item.GuID = reader.ReadElementContentAsInt();
                            }
                            if (reader.ReadToFollowing("pubDate"))
                            {
                                item.PubDate = StringToDateTime(reader.ReadElementContentAsString());
                            }

                            // Add the new item
                            _channel.Items.Add(item);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _channel = null;
                }
            }
            
            return _channel;
        }

        public static void AddItem(RssItem item)
        {
            GetRssFeed(Path);
            if (_channel != null)
            {
                _channel.Items.Add(item);
                _channel.LastBuild = item.PubDate;

                WriteRss(Path);
            }
        }

        public static void EditChannel(RssChannel channel)
        {
            GetRssFeed(Path);

            // If _channel is still zero, rss feed does not jet exist
            if (_channel == null)
            {
                _channel = new RssChannel();
                _channel.Items = new List<RssItem>();
                _channel.PubDate = channel.PubDate;
            }

            _channel.Title = channel.Title;
            _channel.Description = channel.Description;
            _channel.Link = channel.Link;
            _channel.LastBuild = channel.LastBuild;
            _channel.Ttl = channel.Ttl;

            WriteRss(Path);
        }

        public static void WriteRss(string path)
        {
            Path = path;

            if (_channel != null)
            {
                // Write RSS
                StreamWriter file = new StreamWriter(path);

                // Write Start
                file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                file.WriteLine("<rss version=\"2.0\">");
                file.WriteLine("<channel>");

                // Write Channel info
                file.WriteLine("\t<title>"+_channel.Title+"</title>");
                file.WriteLine("\t<description>" + _channel.Description + "</description>");
                file.WriteLine("\t<link>" + _channel.Link + "</link>");
                file.WriteLine("\t<lastBuildDate>" + DateTimeToString(_channel.LastBuild) + "</lastBuildDate>");
                file.WriteLine("\t<pubDate>" + DateTimeToString(_channel.PubDate) + "</pubDate>");
                file.WriteLine("\t<ttl>" + _channel.Ttl.ToString() + "</ttl>");

                // Write items
                foreach (var item in _channel.Items)
                {
                    file.WriteLine("\t<item>");
                    file.WriteLine("\t\t<title>" + item.Title + "</title>");
                    file.WriteLine("\t\t<description>" + item.Description + "</description>");
                    file.WriteLine("\t\t<link>" + item.Link + "</link>");
                    file.WriteLine("\t\t<guid>" + item.GuID + "</guid>");
                    file.WriteLine("\t\t<pubDate>" + DateTimeToString(item.PubDate) + "</pubDate>");
                    file.WriteLine("\t</item>");
                }

                // Write End
                file.WriteLine("</channel>");
                file.WriteLine("</rss>");

                // Close the file
                file.Close();
            }
        }

        public static DateTime StringToDateTime(string input)
        {
            input = input.Substring(5, 20);
            return DateTime.ParseExact(input, "dd MMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"));
            //return DateTime.ParseExact(input, "ddd, dd MMM yyyy HH:mm:ss zz00", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static string DateTimeToString(DateTime input)
        {
            return input.ToString("ddd, dd MMM yyyy HH:mm:ss +0000", CultureInfo.CreateSpecificCulture("en-US"));
            //return DateTime.ParseExact(input, "ddd, dd MMM yyyy HH:mm:ss zz00", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}