using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAdministration.Model
{
    class LineUp
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private DateRange _Date;

        public DateRange Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private Stage _Stage;

        public Stage Stage
        {
            get { return _Stage; }
            set { _Stage = value; }
        }

        private Band _Band;

        public Band Band
        {
            get { return _Band; }
            set { _Band = value; }
        }
    }
}
