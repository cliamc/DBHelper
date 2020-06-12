using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.FileHelper
{
    public class LineColumn
    {
        private string _cValue;

        public string cValue
        {
            get { return _cValue.TrimEnd(' '); }

            set { _cValue = value; }
        }

    } // class
}
