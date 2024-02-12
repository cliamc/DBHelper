using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class EpicorVPartSearch
    {
        private AccessSQL dbAccess = new AccessSQL();

        public EpicorVPartSearch()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
        }

        public bool IsActive(string pn)
        {
            bool retVal = false;

            string sqlCmd = string.Format("select Status from v_PartSearch_Class where PartNum = '{0}'", pn);

            dbAccess.SetQueryCmd(sqlCmd);
            string ob = (string)dbAccess.GetASingleValue();
            if (ob.Equals("Active", StringComparison.OrdinalIgnoreCase))
            {
                retVal = true;
            }

            return retVal;
        }

        public bool IsSubGroup(string pn)
        {
            bool retVal = false;

///            string sqlCmd = string.Format("select * from v_PartSearch_Class where PartNum = '{0}' and ([Group] = 'SUB' or ClassID = 'SUB') and Status = 'Active'", pn);
            string sqlCmd = string.Format("select * from v_PartSearch_Class where PartNum = '{0}' and ([Group] IN ('SUB', 'SUBACL', 'SUBDCL', 'SUBOTH', 'SUBPOW') or ClassID = 'SUB') and Status = 'Active'", pn);

            dbAccess.SetQueryCmd(sqlCmd);
            object ob = dbAccess.GetASingleValue();
            if (ob != null)
            {
                retVal = true;
            }

            return retVal;
        }

        public bool IsSubGroupAll(string pn)
        {
            bool retVal = false;

            ///            string sqlCmd = string.Format("select * from v_PartSearch_Class where PartNum = '{0}' and ([Group] = 'SUB' or ClassID = 'SUB') and Status = 'Active'", pn);
            string sqlCmd = string.Format("select * from v_PartSearch_Class where PartNum = '{0}' and ([Group] IN ('SUB', 'SUBACL', 'SUBDCL', 'SUBOTH', 'SUBPOW') or ClassID = 'SUB')", pn);

            dbAccess.SetQueryCmd(sqlCmd);
            object ob = dbAccess.GetASingleValue();
            if (ob != null)
            {
                retVal = true;
            }

            return retVal;
        }

        // Return true means the part number given is a subassembly component
        public bool CheckSubRohs(string pn, out bool rohs, bool isRMA)
        {
            bool ret = false;
            rohs = false;

            string sqlCmd = "";
            if (isRMA)
            {
                sqlCmd = string.Format("select [Group], ClassID, RoHS from v_PartSearch_Class where PartNum = '{0}'", pn);
            }
            else
            {
                sqlCmd = string.Format("select [Group], ClassID, RoHS from v_PartSearch_Class where PartNum = '{0}' and Status = 'Active'", pn);
            }
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string grp = (string)dr["Group"];
                string gid = (string)dr["ClassID"];
                string rf = (string)dr["RoHS"];

                if ( (grp == "SUB") || (gid == "SUB") )
                {
                    ret = true;
                }
                else
                {
                    if ( (rf == "Y") || (rf == "E") )                         // Exempt, No, Pending, Yes
                    {
                        rohs = true;
                    }
                }
            }

            return ret;
        }

    } // class
}
