using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class EpicorFlattenedBOM
    {
        private AccessSQL dbAccess = new AccessSQL();

        public EpicorFlattenedBOM()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
        }

        public bool GetSubBit(string pn, string rev)
        {
            string sqlCmd = string.Format("select top 1 BOMIsSub from amc.FlattenedBOM where BOMPartNum = '{0}' and BOMRevisionNum = '{1}'", pn, rev);

            dbAccess.SetQueryCmd(sqlCmd);
            object oj = dbAccess.GetASingleValue();
            bool rt = false;

            if (oj == null) rt = false;
            else rt = (bool)oj;

            return rt;
        }

    } // class
}
