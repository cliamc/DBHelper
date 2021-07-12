using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataPldLog
    {
        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataPldLog()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public bool InsertLogRecord(string mode, string model, string wo, string qty, string qtyPassed, string fileName, string memoryType, string manuName, string action, string device)
        {
            bool ret = false;

            string station = Environment.MachineName.ToUpper();
            string operatorName = Environment.UserName.ToUpper();
            string strDate = DateTime.Now.ToString();

            try
            {
                string sqlCmd = @"INSERT INTO [PldLog] ([ProgDate], [Mode], [Model], [Job], [Qty], [QtyPassed], [JedFile], [Type], [Mfg], [Actions], [Device], [Station], [Operator]) VALUES ('";
                sqlCmd += strDate + "', '" + mode + "', '" + model + "', '" + wo + "', '" + qty + "', '" + qtyPassed + "', '" + fileName + "', '" + memoryType + "', '" + manuName + "', '" +
                          action + "', '" + device + "', '" + station + "', '" + operatorName + "')";

                dbAccess.SetQueryCmd(sqlCmd);
                dbAccess.RunSQLcmd();

                ret = true;
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

    } // class
}
