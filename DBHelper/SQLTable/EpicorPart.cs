using System.Data;

namespace DBHelper.SQLTable
{
    public class EpicorPart
    {
        private string AMCPartNumber = "";

        private AccessSQL dbAccess = new AccessSQL();

        public EpicorPart()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
        }

        public DataTable GetAMCPartNumber(string pn)
        {
            string sqlCmd = string.Format("select Character01, Character02, Character03 from Part where PartNum = '{0}'", pn);

            dbAccess.SetQueryCmd(sqlCmd);

            ///DataTable dt = dbAccess.ReadDbData();
            DataTable dt = dbAccess.LoadDbData();

            return dt;
        }

        public string GetPNnoSNtrack(string pn)
        {
            string sqlCmd = string.Format("select Partnum from Part where TypeCode = 'M' and InActive = '0' and TrackSerialNum = '0' and PartNum = '{0}'", pn);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                return (string)retVal;
            }

            return "NOT_FOUND";
        }

    } // class
}
