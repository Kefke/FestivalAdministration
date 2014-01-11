using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace StoreApp.DataModel
{
    public sealed class DataSource
    {
        private static DataSource _dataSource = new DataSource();
        
        private ObservableCollection<Genre> _genres = new ObservableCollection<Genre>();

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
        }

        private ObservableCollection<TimeSlot> _timeSlots = new ObservableCollection<TimeSlot>();

        public ObservableCollection<TimeSlot> TimeSlots
        {
            get { return _timeSlots; }
        }

        public static IEnumerable<Genre> GetGenres(string uniqueId)
        {
            if (!uniqueId.Equals("Genres")) throw new ArgumentException("Only 'Genres' is supported as a collection of genres");

            return _dataSource.Genres;
        }

        public static IEnumerable<TimeSlot> GetTimeSlots(string uniqueId)
        {
            if (!uniqueId.Equals("TimeSlots")) throw new ArgumentException("Only 'TimeSlots' is supported as a collection of timeslots");

            return _dataSource.TimeSlots;
        }

        public static Genre GetGenre(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _dataSource.Genres.Where((genre) => genre.ID.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static TimeSlot GetTimeSlot(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _dataSource.TimeSlots.Where((slot) => slot.ID.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static Band GetBand(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _dataSource.Genres.SelectMany(genre => genre.Bands).Where((band) => band.ID.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task LoadJson()
        {
            // Retrieve recipe data from Azure
            var client = new HttpClient();
            //client.MaxResponseContentBufferSize = 1024 * 1024 * 10; // Read up to 10 MB of data
            var response = await client.GetAsync(new Uri("http://localhost:17304/"));
            var result = await response.Content.ReadAsStringAsync();

            // Parse the JSON bands data
            var bands = JsonArray.Parse(result);

            // Convert the JSON objects into DataItems and DataGroups
            ParseGenres(bands);
        }

        private static void ParseGenres(JsonArray array)
        {
            foreach (var item in array)
            {
                var obj = item.GetObject();
                Band band = new Band();
                band.Genres = new ObservableCollection<Genre>();

                foreach (var key in obj.Keys)
                {
                    IJsonValue val;
                    if (!obj.TryGetValue(key, out val))
                        continue;

                    // Get The band
                    switch (key)
                    {
                        case "ID":
                            band.ID = (int)val.GetNumber();
                            break;
                        case "Name":
                            band.Name = val.GetString();
                            break;
                        case "Twitter":
                            band.Twitter = val.GetString();
                            break;
                        case "Facebook":
                            band.Facebook = val.GetString();
                            break;
                        case "Picture":
                            var arr = val.GetArray();
                            band.Picture = new byte[arr.Count];
                            int index = 0;
                            foreach (var itm in arr)
                            {
                                band.Picture[index] = (byte)itm.GetNumber();
                                ++index;
                            }
                            break;
                        case "Description":
                            band.Facebook = val.GetString();
                            break;
                        case "Genres":
                            if (val.GetArray().Count > 0)
                            {
                                for (uint i = 0; i < val.GetArray().Count; ++i)
                                {
                                    JsonObject bandgenre = val.GetArray().GetObjectAt(i);
                                    Genre genre = null;
                                    IJsonValue genreID;
                                    if (!bandgenre.TryGetValue("ID", out genreID))
                                        continue;

                                    int gID = (int)genreID.GetNumber();
                                    //genre = _bandDataSource.AllGenres.FirstOrDefault(c => c.ID.Equals(genreID.GetString()));
                                    foreach (var g in _dataSource.Genres)
                                    {
                                        if (g.ID == gID)
                                            genre = g;
                                    }

                                    if (genre == null)
                                        genre = CreateGenre(bandgenre);

                                    band.Genres.Add(genre);
                                }
                            }
                            break;
                    }
                }

                // Update the band in the genres
                if (band.Genres != null)
                {
                    foreach (var genre in band.Genres)
                    {
                        if (genre.Bands == null)
                            genre.Bands = new ObservableCollection<Band>();

                        genre.Bands.Add(band);
                    }
                }
            }
        }

        private static Genre CreateGenre(JsonObject obj)
        {
            Genre genre = new Genre();

            foreach (var key in obj.Keys)
            {
                IJsonValue val;
                if (!obj.TryGetValue(key, out val))
                    continue;

                switch (key)
                {
                    case "ID":
                        genre.ID = (int)val.GetNumber();
                        break;
                    case "Name":
                        genre.Name = val.GetString();
                        break;
                }
            }

            _dataSource.Genres.Add(genre);
            return genre;
        }
    }
}
