using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataPLDDevice
    {
        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataPLDDevice()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public DataTable GetPLDparams(string amcPN)
        {
            string sqlCmd = string.Format("select * from PLDDevice where AMCPartNumber = '{0}'", amcPN);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            return retVal;
        }

    } // class
}
