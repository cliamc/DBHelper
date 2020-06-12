using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLDataLoadTbl
{
    public class ProdmanHistory
    {
        private AccessSQL dbAccess = new AccessSQL();

        public ProdmanHistory()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrProdmanData());
        }

        public DataTable SelectNotes()
        {
            string sqlCmd = string.Format("select partnumber, [version], notes, testnotes, fanotes, gennotes from history order by partnumber");
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            return retVal;
        }

    }
}
