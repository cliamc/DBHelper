using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class EpicorVPartSearch
    {
        private AccessSQL dbAccess = new AccessSQL();

        public EpicorVPartSearch()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
        }

        public bool IsSubGroup(string pn)
        {
            bool retVal = false;

            string sqlCmd = string.Format("select * from v_PartSearch where PartNum = '{0}' and [Group] = 'SUB' and Status = 'Active'", pn);
            dbAccess.SetQueryCmd(sqlCmd);
            object ob = dbAccess.GetASingleValue();
            if (ob != null)
            {
                retVal = true;
            }

            return retVal;
        }


    } // class
}
