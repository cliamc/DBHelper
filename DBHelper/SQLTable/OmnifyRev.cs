using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class OmnifyRev
    {
        string connStr1 = "Data Source=amc-plm-sql\\omnifyplm;Initial Catalog=";
        string connStr2 = ";Persist Security Info=True;User ID=EpicorLinkedServerUser;Password=EpicorLinkedServerUser";
        int ID = 0;

        private AccessSQL dbAccess = new AccessSQL();

        public OmnifyRev()
        {
            MfgDataApplicationParam mdap = new MfgDataApplicationParam("DocViewer");
            string esrv = mdap.SelectApplicationParam("EmpowerServer");
            string connStr = connStr1 + esrv + connStr2;
            dbAccess.SetConnStr(connStr);
            //dbAccess.SetConnStr(DBConnectionStr.SQLOmnifyConnStr());
        }

        public int GetID(string itemId, string rev)
        {
            string sqlCmd = string.Format("select ID from Rev where ItemID = {0} and Rev = '{1}'", itemId, rev);

            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                this.ID = (int)retVal;
                return (int)retVal;
            }
            else
            {
                return -1;
            }
        }

    } // class
}
