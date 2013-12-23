using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FestivalAdministration.Converter
{
    class ImagePathToByteArrayConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imgPath = (string)value;
            FileStream fs = new FileStream(imgPath, FileMode.Open);
            if (fs.Length == 0) return null;

            byte[] result = new byte[fs.Length];
            fs.Read(result, 0, (int)fs.Length);

            return result;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
            //throw new NotImplementedException();
        }
    }
}
