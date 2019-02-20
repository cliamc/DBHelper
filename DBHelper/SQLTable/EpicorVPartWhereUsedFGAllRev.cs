using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{

    public class EpicorVPartWhereUsedFGAllRev
    {
        private AccessSQL dbAccess = new AccessSQL();

        private string partNum = "";
        private string partRev = "";

        public List<string> AllParts = new List<string>();

        public EpicorVPartWhereUsedFGAllRev(string pn, string ver)
        {
            this.partNum = pn;
            this.partRev = ver;

            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());

            SetAllParts();
        }

        private void SetAllParts()
        {
            string sqlCmd = string.Format("select * from v_PartWhereUsedFG_AllRev where PartNum = '{0}' and RevisionNum = '{1}'", this.partNum, this.partRev);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            if (dt != null)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    string tmp = (string)dRow["MtlPartNum"];

                    AllParts.Add(tmp);
                }
            }
        }


    } // class
}
