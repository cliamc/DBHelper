using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class EpicorVJobInformation
    {
        private AccessSQL dbAccess = new AccessSQL();

        private string JobNum;            // aka workcode, work order

        public EpicorVJobInformation(string JN)
        {
            this.JobNum = JN;

            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
        }

        public DataTable SelectTbl()
        {
            string sqlCmd = string.Format("select * from v_EpicorAccess_JobInformation where JobNum = '{0}'", this.JobNum);

            dbAccess.SetQueryCmd(sqlCmd);

            DataTable dt = dbAccess.ReadDbData();
            ///DataTable dt = dbAccess.LoadDbData();

            return dt;
        }

        public bool ValidateVersion(string Ver)
        {
            bool ret = false;

            DataTable dt = SelectTbl();
            if (dt.Rows.Count == 1)
            {
                if ( Ver.Trim() == dt.Rows[0]["RevisionNum"].ToString().Trim() )
                    ret = true;
            }

            return ret;
        }

    } // class
}
