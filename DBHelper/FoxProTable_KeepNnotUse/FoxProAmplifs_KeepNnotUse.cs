using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.FoxProTable
{
    public class FoxProAmplifs
    {
        private AccessFoxProODBC dbAccess = new AccessFoxProODBC();

        private string workCode;

        public FoxProAmplifs(string wc)
        {
            this.workCode = wc;

            dbAccess.SetConnStr(DBConnectionStr.FoxProODBCConnStr("amplifs"));
            //dbAccess.SetConnStr(DBConnectionStr.FoxProOLEConnStr("amplifs"));
        }

        public DataTable SelectTbl()
        {
            string adoCmd = string.Format("select * from Amplifs where Workcode = '{0}'", this.workCode);
            dbAccess.SetQueryCmd(adoCmd);
            DataTable dt = dbAccess.ReadDbData();

            return dt;
        }

    } // class
}
