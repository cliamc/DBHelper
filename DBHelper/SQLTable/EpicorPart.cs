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

        //public string GetAMCPartNumber(string pn)
        //{
        //    string sqlCmd = string.Format("select Character01 from Part where PartNum = '{0}'", pn);
        //    dbAccess.SetQueryCmd(sqlCmd);
        //    object retVal = dbAccess.GetASingleValue();

        //    if (retVal != null)
        //    {
        //        this.AMCPartNumber = (string)retVal;
        //    }
        //    else
        //    {
        //        this.AMCPartNumber = pn;
        //    }

        //    return this.AMCPartNumber;
        //}

        public DataTable GetAMCPartNumber(string pn)
        {
            string sqlCmd = string.Format("select Character01, Character02, Character03 from Part where PartNum = '{0}'", pn);

            dbAccess.SetQueryCmd(sqlCmd);

            ///DataTable dt = dbAccess.ReadDbData();
            DataTable dt = dbAccess.LoadDbData();

            return dt;
        }

    } // class
}
