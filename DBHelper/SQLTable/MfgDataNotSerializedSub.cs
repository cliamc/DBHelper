using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataNotSerializedSub
    {
        private string PartNumber = "";
        private string PartVersion = "";

        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataNotSerializedSub()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public bool IsFound(string pn)
        {
            bool retVal = false;

            string sqlCmd = string.Format("select * from NotSerializedSub where PartNumber = '{0}'", pn.Trim());
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
