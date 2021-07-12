using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataPLDParameter
    {
        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataPLDParameter()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public bool InsertRecord(string job, string pn, string rev, string qty, string amcPN, string fileName, string device, string memoryType, string manuName, string amcPN2, string fileName2, string device2, string memoryType2, string manuName2)
        {
            bool ret = false;

            string station = Environment.MachineName.ToUpper();
            string operatorName = Environment.UserName.ToUpper();
            string strDate = DateTime.Now.ToString();

            try
            {
                string sqlCmd =
@"INSERT INTO [PLDParameter] ([Job], [PartNumber], [PartVersion], [Quantity], [AMCpartNumber], [JEDfileName], [DeviceName], [MemoryType], [ManufactureName], [AMCpartNumber2], [JEDfileName2], [DeviceName2], [MemoryType2], [ManufactureName2],[CreateDate], [CreateUser], [CreateComputer]) VALUES ('";
                sqlCmd += job + "', '" + pn + "', '" + rev + "', '" + qty + "', '" + amcPN + "', '" + fileName + "', '" + device + "', '" + memoryType + "', '" + manuName + "', '" + amcPN2 + "', '" + fileName2 + "', '" + device2 + "', '" + memoryType2 + "', '" + manuName2 + "', '" +
                          strDate + "', '" + operatorName + "', '" + station + "')";

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
