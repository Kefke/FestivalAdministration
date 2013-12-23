using FestivalAdministration.Converter;
using FestivalAdministration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class LineupVM : ObservableObject, IPage
    {
        public LineupVM()
        {
            Bands = Band.GetBands();

            /*string path = "C:\\Users\\Kefke\\Pictures\\sabaton.jpg";
            ImagePathToByteArrayConverter conv = new ImagePathToByteArrayConverter();
            byte[] binaryImage = (byte[])conv.Convert(path, null, null, null);
            Band.AddBand("Sabaton", "https://twitter.com/sabaton", "https://www.facebook.com/sabaton", binaryImage, "Sabaton [ˈsabaˌton] are a heavy metal band from Falun, Sweden formed in 1999. The band's main lyrical themes are based on war and historical battles. ");*/
        }

        public string Name
        {
            get { return "Line-up"; }
        }

        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; OnPropertyChanged("Bands"); }
        }
        
    }
}
