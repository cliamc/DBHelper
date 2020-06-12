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
        private List<string> uniqueParts = new List<string>();

        public EpicorVPartWhereUsedFGAllRev(string pn, string ver)
        {
            this.partNum = pn;
            this.partRev = ver;

            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());

            SetAllParts();
        }

        private void SetAllParts()
        {
            string sqlCmd = string.Format("select * from v_PartWhereUsedFG_AllRev where PartNum = '{0}' and RevisionNum = '{1}' order by MtlPartNum", this.partNum, this.partRev);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    string tQty = (string)dRow["QtyPer"].ToString();
                    int pos = tQty.IndexOf(".");
                    string intPortion = tQty.Substring(0, pos);
                    int tfloat = Convert.ToInt32(intPortion);
                    string tmp = (string)dRow["MtlPartNum"];

                    uniqueParts.Add(tmp);
                    for (int i = 1; i <= tfloat; i++)
                    {
                        AllParts.Add(tmp);
                    }
                }
            }
        }

        public List<string> GetUniqueParts()
        {
            return this.uniqueParts;
        }

    } // class
}
