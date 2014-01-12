using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

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

        private ObservableCollection<Stage> _stages = new ObservableCollection<Stage>();

        public ObservableCollection<Stage> Stages
        {
            get { return _stages; }
        }

        private ObservableCollection<Band> _bands = new ObservableCollection<Band>();

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
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

        public static IEnumerable<Stage> GetStages(string uniqueId)
        {
            if (!uniqueId.Equals("Stages")) throw new ArgumentException("Only 'Stages' is supported as a collection of timeslots");

            return _dataSource.Stages;
        }

        public static IEnumerable<Band> GetBands(string uniqueId)
        {
            if (!uniqueId.Equals("Bands")) throw new ArgumentException("Only 'Bands' is supported as a collection of bands");

            return _dataSource.Bands;
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
            /*var matches = _dataSource.Genres.SelectMany(genre => genre.Bands).Where((band) => band.ID.Equals(uniqueId));
            if (matches.Count() >= 1) return matches.First();
            return null;*/
            foreach (var genre in _dataSource.Genres)
            {
                foreach (var band in genre.Bands)
                {
                    if (uniqueId == band.ID.ToString()) 
                        return band;
                }
            }
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
                band.TimeSlots = new ObservableCollection<TimeSlot>();

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
                            var bytes = new byte[arr.Count];
                            int index = 0;
                            foreach (var itm in arr)
                            {
                                bytes[index] = (byte)itm.GetNumber();
                                ++index;
                            }

                            /*MemoryStream ms = new MemoryStream(bytes);
                            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                            ms.CopyTo(stream.AsStreamForWrite());*/
                            /*var stream = new InMemoryRandomAccessStream();
                            stream.WriteAsync(bytes.AsBuffer());
                            stream.Seek(0);

                            var picture = new BitmapImage();
                            picture.SetSource(stream);

                            band.Picture = picture;*/
                            band.Picture = ByteToImage(bytes).Result;
                            break;
                        case "Description":
                            band.Description = val.GetString();
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
                        case "TimeSlots":
                            if (val.GetArray().Count > 0)
                            {
                                for (uint i = 0; i < val.GetArray().Count; ++i)
                                {
                                    JsonObject timeslot = val.GetArray().GetObjectAt(i);
                                    TimeSlot slot = CreateTimeSlot(timeslot);

                                    band.TimeSlots.Add(slot);
                                }
                            }
                            break;
                    }
                }
                // Save band in bands
                _dataSource.Bands.Add(band);

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

                // Update the band in the timeslots
                if (band.TimeSlots != null)
                {
                    foreach (var slot in band.TimeSlots)
                    {
                        slot.Band = band;
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

        private static TimeSlot CreateTimeSlot(JsonObject obj)
        {
            TimeSlot slot = new TimeSlot();

            foreach (var key in obj.Keys)
            {
                IJsonValue val;
                if (!obj.TryGetValue(key, out val))
                    continue;

                switch (key)
                {
                    case "ID":
                        slot.ID = (int)val.GetNumber();
                        break;
                    case "Stage":
                        JsonObject stageTimeSlot = val.GetObject();
                        Stage stage = null;
                        IJsonValue stageID;
                        if (!stageTimeSlot.TryGetValue("ID", out stageID))
                            continue;

                        int sID = (int)stageID.GetNumber();
                        foreach (var s in _dataSource.Stages)
                        {
                            if (s.ID == sID)
                                stage = s;
                        }

                        if (stage == null)
                            stage = CreateStage(stageTimeSlot);

                        slot.Stage = stage;
                        break;
                    case "StartDate":
                        slot.StartDate = JsonToDateTime(val);
                        break;
                    case "EndDate":
                        slot.EndDate = JsonToDateTime(val);
                        break;
                }
            }

            // Update the timeslots in the stages
            if (slot.Stage != null)
            {
                if (slot.Stage.TimeSlots == null)
                    slot.Stage.TimeSlots = new ObservableCollection<TimeSlot>();

                slot.Stage.TimeSlots.Add(slot);
            }

            _dataSource.TimeSlots.Add(slot);
            return slot;
        }

        private static Stage CreateStage(JsonObject obj)
        {
            Stage stage = new Stage();

            foreach (var key in obj.Keys)
            {
                IJsonValue val;
                if (!obj.TryGetValue(key, out val))
                    continue;

                switch (key)
                {
                    case "ID":
                        stage.ID = (int)val.GetNumber();
                        break;
                    case "Name":
                        stage.Name = val.GetString();
                        break;
                }
            }

            _dataSource.Stages.Add(stage);
            return stage;
        }

        private static DateTime JsonToDateTime(IJsonValue val)
        {
            try
            {
                DateTime result = new DateTime(1970, 1, 1);

                string json = val.Stringify();
                string datestring = json.Split("()".ToCharArray())[1];
                double millis = Convert.ToDouble(datestring);

                return result.AddMilliseconds(millis);
            }
            catch (Exception ex)
            {
            }
            return new DateTime();
        }

        private static async Task<BitmapImage> ByteToImage(byte[] imageBytes)
        {
            var image = new BitmapImage();
            using (var randomAccessStream = new InMemoryRandomAccessStream())
            {
                var dw = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
                dw.WriteBytes(imageBytes);
                await dw.StoreAsync();
                image.SetSourceAsync(randomAccessStream);
            }
            return image;
        }
    }
}
