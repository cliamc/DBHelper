using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLDataLoadTbl
{
    public class ProdmanBeta
    {
        private AccessSQL dbAccess = new AccessSQL();

        public ProdmanBeta()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrProdmanData());
        }

        public DataTable SelectNotes()
        {
            string sqlCmd = string.Format("select partnumber, [version], notes, testnotes, fanotes, gennotes from beta");
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            return retVal;
        }

    } // class
}
