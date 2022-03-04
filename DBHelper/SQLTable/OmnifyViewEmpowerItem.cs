using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class OmnifyViewEmpowerItem
    {
        string connStr1 = "Data Source=amc-plm-sql\\omnifyplm;Initial Catalog=";
        string connStr2 = ";Persist Security Info=True;User ID=EpicorLinkedServerUser;Password=EpicorLinkedServerUser";
        string ol = "";

        private AccessSQL dbAccess = new AccessSQL();

        public OmnifyViewEmpowerItem()
        {
            MfgDataApplicationParam mdap = new MfgDataApplicationParam("DocViewer");
            string esrv = mdap.SelectApplicationParam("EmpowerServer");
            string connStr = connStr1 + esrv + connStr2;
            dbAccess.SetConnStr(connStr);
            //dbAccess.SetConnStr(DBConnectionStr.SQLOmnifyConnStr());
        }

        public string GetOL(string pn, string rev)
        {
            string sqlCmd = string.Format("select [Object Link] from view_EmpowerItem where [Part Number] = '{0}' and Revision = '{1}'", pn, rev);

            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                this.ol = retVal.ToString();
                return retVal.ToString();
            }
            else
            {
                return "";
            }
        }

    } // class
}
