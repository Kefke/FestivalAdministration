using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace StoreApp.Converters
{
    public class BytesToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is byte[])
            {
                byte[] bytes = value as byte[];
                MemoryStream ms = new MemoryStream(bytes);
                InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                ms.CopyTo(stream.AsStreamForWrite());

                BitmapImage image = new BitmapImage();

                image.SetSource(stream);

                return image;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
