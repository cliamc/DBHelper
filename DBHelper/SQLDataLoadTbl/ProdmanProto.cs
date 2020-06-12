using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLDataLoadTbl
{
    public class ProdmanProto
    {
        private AccessSQL dbAccess = new AccessSQL();

        public ProdmanProto()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrProdmanData());
        }

        public DataTable SelectNotes()
        {
            string sqlCmd = string.Format("select partnumber, [version], notes, testnotes, fanotes, gennotes from proto");
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            return retVal;
        }

    } // class
}
