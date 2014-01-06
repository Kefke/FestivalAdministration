using MetroApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace MetroApp.DataModel
{
    public sealed class BandDataSource
    {
        private static BandDataSource _bandDataSource = new BandDataSource();

        private ObservableCollection<Genre> _allGenres = new ObservableCollection<Genre>();
        public ObservableCollection<Genre> AllGenres
        {
            get { return this._allGenres; }
        }

        public static IEnumerable<Genre> GetGenres(string uniqueId)
        {
            if (!uniqueId.Equals("AllGenres")) throw new ArgumentException("Only 'AllGenres' is supported as a collection of genres");

            return _bandDataSource.AllGenres;
        }

        public static Genre GetGenre(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _bandDataSource.AllGenres.Where((genre) => genre.ID.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static Band GetBand(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _bandDataSource.AllGenres.SelectMany(genres => genres.Bands).Where((band) => band.ID.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task LoadGenres()
        {
            // Retrieve recipe data from Azure
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 1024 * 1024 * 10; // Read up to 10 MB of data
            var response = await client.GetAsync(new Uri("http://localhost:17304/JSON/genres"));
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
                Genre genre = null;

                foreach (var key in obj.Keys)
                {
                    IJsonValue val;
                    if (!obj.TryGetValue(key, out val))
                        continue;

                    switch (key)
                    {
                        case "ID":
                            band.ID = (int)val.GetNumber();
                            break;
                        case "Name":
                            band.Name = val.GetString();
                            break;
                        case "Picture":
                            var arr = val.GetArray();
                            band.Picture = new byte[arr.Count];
                            int index = 0;
                            foreach (var itm in arr)
                            {
                                band.Picture[index] = (byte)itm.GetNumber();
                            }
                            break;
                        case "Genres":
                            if (val.GetArray().Count > 0)
                            {
                                var bandgenre = val.GetArray().GetObjectAt(0);

                                IJsonValue genreID;
                                if (!bandgenre.TryGetValue("ID", out genreID))
                                    continue;

                                genre = _bandDataSource.AllGenres.FirstOrDefault(c => c.ID.Equals(genreID.GetString()));

                                if (genre == null)
                                    genre = CreateGenre(bandgenre);

                                band.Genre = genre;
                            }
                            break;
                    }
                }

                if (genre != null)
                    genre.Bands.Add(band);
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

            _bandDataSource.AllGenres.Add(genre);
            return genre;
        }
    }
}
