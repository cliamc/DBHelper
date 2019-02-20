using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataApplicationParam
    {
        private string AppName = "";

        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataApplicationParam(string an)
        {
            this.AppName = an;

            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public DataTable SelectTbl()
        {
            string sqlCmd = string.Format("select ParamName, ParamValue from ApplicationParam where Application = '{0}'", this.AppName);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            return retVal;
        }

    } // class
}
