using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.ViewModel
{
    class GenreVM: ObservableObject, IPage
    {
        public GenreVM()
        {
        }

        public string Name
        {
            get { return "Genre Detail"; }
        }
    }
}
