using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{

    public class EpicorSNTran
    {
        private AccessSQL dbAccess = new AccessSQL();

        public EpicorSNTran()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
        }

        public DataTable GetScrappedStatus(string sn)
        {
            string sqlCmd = string.Format("select top 1 TranType, SNStatus from [dbo].SNTran where SerialNumber = '{0}' order by TranNum desc", sn);

            dbAccess.SetQueryCmd(sqlCmd);

            DataTable dt = dbAccess.LoadDbData();

            return dt;
        }

    } // class
}
