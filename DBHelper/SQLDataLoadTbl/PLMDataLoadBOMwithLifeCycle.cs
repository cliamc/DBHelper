using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class PLMDataLoadBOMwithLifeCycle
    {
        private AccessSQL dbAccess = new AccessSQL();

        public PLMDataLoadBOMwithLifeCycle()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public string GetLifeCycle(string part, string ver)
        {
            string ret = "";

            string sqlCmd = string.Format("select top 1 LifeCycle from BOMwithLifeCycle where PartNumber = '{0}' and Revision = '{1}'", part, ver);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = (string)retVal;
            }

            return ret;
        }

        public string GetLifeCycle(string part)
        {
            string ret = "";

            string sqlCmd = string.Format("select top 1 LifeCycle from BOMwithLifeCycle where AMC_PartNum = '{0}'", part);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = (string)retVal;
            }

            return ret;
        }

    } // class
}
