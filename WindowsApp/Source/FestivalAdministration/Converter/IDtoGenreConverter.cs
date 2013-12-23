using FestivalAdministration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FestivalAdministration.Converter
{
    class IDtoGenreConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int ID = (int)value;
            int index = Genre.GetIndexByID(ID);
            if (index >= 0)
                return Genre.GetGenres()[index];

            return null;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Genre genre = (Genre)value;
            return genre.ID;
            //throw new NotImplementedException();
        }
    }
}
