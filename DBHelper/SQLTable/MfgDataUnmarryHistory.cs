using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataUnmarryHistory
    {
        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataUnmarryHistory()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public bool InsertRecord(string subSN, string tlSN)
        {
            bool ret = false;
            if (!CheckRecordExist(subSN, tlSN))
            {
                string sqlCmd = string.Format("insert into UnmarryHistory (SubSerialNumber, TL_SerialNumber, UnmarryUser, UnmarryComputer)" +
                           "values ('{0}', '{1}', '{2}', '{3}')", subSN, tlSN, Environment.UserName, Environment.MachineName);
                dbAccess.SetQueryCmd(sqlCmd);
                dbAccess.RunSQLcmd();
                ret = true;
            }

            return ret;
        }

        private bool CheckRecordExist(string subPN, string tlSN)
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from UnmarryHistory where SubSerialNumber = '{0}' and TL_SerialNumber = '{1}'", subPN, tlSN);
            dbAccess.SetQueryCmd(sqlCmd);
            object retRec = dbAccess.GetASingleValue();
            if (retRec != null)
                ret = true;

            return ret;
        }

    } // class
}
