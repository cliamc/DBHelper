using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class EpicorSerialMatch
    {
        private AccessSQL dbAccess = new AccessSQL();

        private string ChildPartNum = "";            // This pair is unique for a record in the table
        private string ChildSerialNo = "";
        private string PreviousChildSerialNo = "";

        private string ParentPartNum = "";
        private string ParentSerialNo = "";

        public EpicorSerialMatch()
        {
            dbAccess.SetConnStr(DBConnectionStr.SQLEpicorConnStr());
        }

        public DataTable SelectTheRecord()
        {
            string sqlCmd = string.Format("select company, parentpartnum, parentserialno, childpartnum, childserialno, datematched from serialmatch where childpartnum = '{0}' and childserialno = '{1}'",
                                          this.ChildPartNum, this.ChildSerialNo);

            dbAccess.SetQueryCmd(sqlCmd);

            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                return (DataTable)retVal;
            }
            else
            {
                return null;
            }
        }

        // Insert or Update a record
        public bool InsertUpdateTbl(string parentPart, string parentSN, string childPart, string childSN, string prevChildSN)
        {
            this.ParentPartNum = parentPart;
            this.ParentSerialNo = parentSN;
            this.ChildPartNum = childPart;
            this.ChildSerialNo = childSN;
            this.PreviousChildSerialNo = prevChildSN;

            //string tmp = string.Format("EpicorSerialMatch.InsertUpdateTbl: {0} | {1} | {2} | {3} | {4}", this.ParentPartNum, this.ParentSerialNo, this.ChildPartNum, this.ChildSerialNo, this.PreviousChildSerialNo);
            //MfgDataTraceRecord.LogRecord(tmp);

            bool ret = false;

            try
            {
                if (CheckRecordExist())
                    ret = UpdateTbl();
                else
                    ret = InsertTbl();
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

        private bool CheckRecordExist()
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from serialmatch where childpartnum = '{0}' and childserialno = '{1}'", this.ChildPartNum, this.PreviousChildSerialNo);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = true;
            }

            return ret;
        }

        private bool UpdateTbl()
        {
            string sqlCmd = string.Format("update serialmatch set childserialno = '{0}', datematched = '{1}' where childpartnum = '{2}' and childserialno = '{3}'",
                                          this.ChildSerialNo, DateTime.Now, this.ChildPartNum, this.PreviousChildSerialNo);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();

            return true;
        }

        private bool InsertTbl()
        {
            bool ret = false;

            string sqlCmd = string.Format("insert into serialmatch (company, parentpartnum, parentserialno, childpartnum, childserialno, datematched) values('AMC', '{0}', '{1}', '{2}', '{3}', '{4}')",
                                           this.ParentPartNum, this.ParentSerialNo, this.ChildPartNum, this.ChildSerialNo, DateTime.Now);    // DateTime.Now.ToString("u") "2000-08-17 23:32:32Z"
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();

            return ret;
        }

    } // class
}
