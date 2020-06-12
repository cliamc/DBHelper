using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataSubPart
    {
        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataSubPart()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public bool CheckPartExist(string pn)
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from SubPart where PartNumber = '{0}'", pn);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = true;
            }

            return ret;
        }

    } // class
}
