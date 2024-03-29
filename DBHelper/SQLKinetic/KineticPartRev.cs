﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLKinetic
{
    public class KineticPartRev
    {
        private string PartNumber = "";
        private string PartRevision = "";

        private AccessSQL dbAccess = new AccessSQL();

        public KineticPartRev()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLKineticConnStr());
        }

        public string GetRevision(string pn)
        {
            string sqlCmd = string.Format("select top 1 RevisionNum from Erp.PartRev where PartNum = '{0}' and Approved = 1 order by RevisionNum desc", pn);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                this.PartRevision = (string)retVal;
            }
            else
            {
                this.PartRevision = "";
            }

            return this.PartRevision;
        }

    } // class
}
