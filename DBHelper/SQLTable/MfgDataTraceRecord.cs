using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public static class MfgDataTraceRecord
    {
        private static AccessSQL dbAccess = new AccessSQL();

        // The static constructor
        static MfgDataTraceRecord()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public static void LogRecord(string theText)
        {
            string sqlCmd = string.Format("insert into TraceRecord (LogText) values ('{0}')", theText);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();
        }
        
    } // class
}
