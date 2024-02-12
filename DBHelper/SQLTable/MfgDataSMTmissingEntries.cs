using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataSMTmissingEntries
    {
        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataSMTmissingEntries()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public DataTable SelectTbl(string pn, string rev)
        {
            string sqlCmd = string.Format("select * from SMTmissingEntries where PartNumber = '{0}' and PartRevision = '{1}' order by CreateTime", pn, rev);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            return dt;
        }

        public bool InsertTbl(string jb, string pn, string rev, string refdes, string cpn)
        {
            bool ret = false;

            string sqlCmd = string.Format("insert into SMTmissingEntries (Job, PartNumber, PartRevision, Refdes, ComponentPartNum, CreateUser, CreateComputer) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                           jb, pn, rev, refdes, cpn, Environment.UserName, Environment.MachineName);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();

            return ret;
        }

        public bool DeleteTbl(string pn, string rev)
        {
            bool ret = false;

            string sqlCmd = string.Format("delete SMTmissingEntries where PartNumber = '{0}' and PartRevision = '{1}'", pn, rev);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();

            return ret;
        }

    } // class
}
