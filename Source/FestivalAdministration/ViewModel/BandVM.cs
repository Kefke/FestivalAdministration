using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class BandVM: ObservableObject, IPage
    {
        public BandVM()
        {
        }

        public string Name
        {
            get { return "Band Detail"; }
        }
    }
}
