using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class OmnifyViewGetItemDocs
    {
        string connStr1 = "Data Source=amc-plm-sql\\omnifyplm;Initial Catalog=";
        string connStr2 = ";Persist Security Info=True;User ID=EpicorLinkedServerUser;Password=EpicorLinkedServerUser";

        private AccessSQL dbAccess = new AccessSQL();

        public OmnifyViewGetItemDocs()
        {
            MfgDataApplicationParam mdap = new MfgDataApplicationParam("DocViewer");
            string esrv = mdap.SelectApplicationParam("EmpowerServer");
            string connStr = connStr1 + esrv + connStr2;
            dbAccess.SetConnStr(connStr);
            //dbAccess.SetConnStr(DBConnectionStr.SQLOmnifyConnStr());
        }

        public string GetPCBfilename(string pn, string rev)
        {
            string sqlCmd = string.Format("select [Document Title] from view_GetItemDocs where [Document Type] = 'PCB' and [Part Number] = '{0}' and [Revision] = '{1}'", pn, rev);

            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                return retVal.ToString();
            }
            else
            {
                return "";
            }
        }

    } // class
}
