using System.Data;

namespace DBHelper.SQLTable
{
    public class MfgDataDevAMLbase
    {

        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataDevAMLbase()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrMfgDataDev);
        }

        public DataTable SelectTbl()
        {
            string sqlCmd = string.Format("select * from AMLbase");
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            return dt;
        }

    } // class
}
