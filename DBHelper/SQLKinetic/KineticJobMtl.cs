using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLKinetic
{
    public class KineticJobMtl
    {
        private AccessSQL dbAccess = new AccessSQL();

        private string JobNum;            // aka workcode

        public KineticJobMtl(string JN)
        {
            this.JobNum = JN;

            dbAccess.SetConnStr(DBConnectionStr.SQLKineticConnStr());
        }

        public DataTable SelectTbl()
        {
            string sqlCmd = string.Format("select PartNum, QtyPer, ReqDate, EstUnitCost from Erp.JobMtl where JobNum = '{0}' and PartNum like '%=%'", this.JobNum);

            dbAccess.SetQueryCmd(sqlCmd);

            ///DataTable dt = dbAccess.ReadDbData();
            DataTable dt = dbAccess.LoadDbData();

            return dt;
        }

        public string GetSubRevision(string PN)
        {
            string sqlCmd = string.Format("select RevisionNum from Erp.JobMtl where JobNum = '{0}' and PartNum = '{1}'", this.JobNum, PN);

            dbAccess.SetQueryCmd(sqlCmd);
            string rt = (string)dbAccess.GetASingleValue();

            return rt;
        }

    } // class
}
