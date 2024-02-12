using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class EpicorVJobInformationAll
    {
        private AccessSQL dbAccess = new AccessSQL();

        private string JobNum;            // aka workcode, work order

        public EpicorVJobInformationAll(string JN)
        {
            this.JobNum = JN;

            //dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
            // Always use Epicor production database in order to get current data. This class does read access only and is no harm to data.
            dbAccess.SetConnStr(DBConnectionStr.ConnStrEpicorProd);
        }

        public DataTable SelectTbl()
        {
            string sqlCmd = string.Format("select * from v_EpicorAccess_JobInformationNotReleased where JobNum = '{0}'", this.JobNum);

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
                if (Ver.Trim() == dt.Rows[0]["RevisionNum"].ToString().Trim())
                    ret = true;
            }

            return ret;
        }

    } // class
}
