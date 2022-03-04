using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class OmnifyEmpowerIDs
    {
        string connStr1 = "Data Source=amc-plm-sql\\omnifyplm;Initial Catalog=";
        string connStr2 = ";Persist Security Info=True;User ID=EpicorLinkedServerUser;Password=EpicorLinkedServerUser";
        string ol = "";

        private AccessSQL dbAccess = new AccessSQL();

        public OmnifyEmpowerIDs()
        {
            MfgDataApplicationParam mdap = new MfgDataApplicationParam("DocViewer");
            string esrv = mdap.SelectApplicationParam("EmpowerServer");
            string connStr = connStr1 + esrv + connStr2;
            dbAccess.SetConnStr(connStr);
            //dbAccess.SetConnStr(DBConnectionStr.SQLOmnifyConnStr());
        }

        public string GetOL(string pn, string rev)
        {
            string sqlCmd = string.Format("SELECT EntryInfo.ID AS ItemID, Rev.ID AS RevID FROM EntryInfo INNER JOIN Rev ON EntryInfo.Rev = Rev.ID WHERE PartNumber = '{0}' AND Rev.Rev = '{1}'", pn, rev);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            if (retVal.Rows.Count != 0)
            {
                string target = "http://amc-plm-app.a-m-c.com/Omnify7/Apps/Desktop/Item/Object.aspx?id=";           // 9667&revid=12677";

                string itemId = retVal.Rows[0]["ItemID"].ToString();
                string revId = retVal.Rows[0]["RevID"].ToString();

                target = target + itemId + "&revid=" + revId;

                return target;
            }
            else
            {
                return "";
            }
        }

    } // class
}
