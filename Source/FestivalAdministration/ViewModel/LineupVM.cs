using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class LineupVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Line-up"; }
        }
    }
}
