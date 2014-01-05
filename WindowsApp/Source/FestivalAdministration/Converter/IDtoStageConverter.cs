using FestivalAdministration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FestivalAdministration.Converter
{
    class IDtoStageConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int ID = (int)value;
            int index = Stage.GetIndexByID(ID);
            if (index >= 0)
                return Stage.GetStages()[index];

            return null;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Stage stage = (Stage)value;
            return stage.ID;
            //throw new NotImplementedException();
        }
    }
}
