using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLKinetic
{
    public class KineticPart
    {
        private string AMCPartNumber = "";

        private AccessSQL dbAccess = new AccessSQL();

        public KineticPart()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLKineticConnStr());
        }

        public DataTable GetAMCPartNumber(string pn)
        {
            //string sqlCmd = string.Format("select Character01, Character02, Character03 from Erp.Part where PartNum = '{0}'", pn);
            string sqlCmd = string.Format("select Character01, Character02, Character03 from Erp.Part_UD where ForeignSysRowID = (select SysRowID from Erp.Part where PartNum = '{0}')", pn);

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
