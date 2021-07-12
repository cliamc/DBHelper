using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLDataLoadTbl
{
    public class ProdmanCurrent
    {
        private AccessSQL dbAccess = new AccessSQL();

        public ProdmanCurrent()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrProdmanData());
        }

        public DataTable SelectNotes()
        {
            //string sqlCmd = string.Format("select partnumber, [version], notes, testnotes, fanotes, gennotes from [current]");
            string sqlCmd = string.Format("select partnumber, [version], notes, testnotes from [current]");

            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            return retVal;
        }

    } // class
}
