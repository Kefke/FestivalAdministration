using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class GeneralVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "General"; }
        }
    }
}
