using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLDataLoadTbl
{
    public class PLMDataLoadEngineerDocExtType
    {
        private AccessSQL dbAccess = new AccessSQL();

        // Class constructor
        public PLMDataLoadEngineerDocExtType()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public string GetDocumentType(string docExt)
        {
            string ret = "";

            string sqlCmd = string.Format("select top 1 DocumentType from EngineerDocExtType where FileExtension like '{0}%' order by FileExtension", docExt);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = (string)retVal;
            }

            return ret;
        }

    }
}
