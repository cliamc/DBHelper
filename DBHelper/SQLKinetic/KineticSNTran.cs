using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLKinetic
{
    public class KineticSNTran
    {
        private AccessSQL dbAccess = new AccessSQL();

        public KineticSNTran()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
        }

        public DataTable GetAMCPartNumber(string pn)
        {
            string sqlCmd = string.Format("select top 1 TranType, SNStatus from [dbo].SNTran where SerialNumber = '{0}' order by TranNum desc", pn);

            dbAccess.SetQueryCmd(sqlCmd);

            DataTable dt = dbAccess.LoadDbData();

            return dt;
        }

    } // class
}
