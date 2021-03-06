﻿using FestivalAdministration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FestivalAdministration.Converter
{
    class IDtoBandConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int ID = (int)value;
            int index = Band.GetIndexByID(ID);
            if (index >= 0)
                return Band.GetBands()[index];

            return null;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Band band = (Band)value;
            return band.ID;
            //throw new NotImplementedException();
        }
    }
}
