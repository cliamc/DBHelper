using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataJob
    {
        private int jID;
        public string jobNum;                                                     // used if insert
        public string PartNumber;                                                 // used if insert
        public string PartVersion;

        private DateTime CreateDate = DateTime.Today;                              // used if insert

        public int Quantity = 0;
        private int InProcessCt = 0;                                                       // used if insert

        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataJob()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public MfgDataJob(string wc, string pn, string ver, int ct)
        {
            this.jobNum = wc;
            this.PartNumber = pn;
            this.PartVersion = ver;

            // How to use Quantity is TBD
            this.InProcessCt = ct;

            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public int GetJID(string wc)
        {
            string sqlCmd = string.Format("select jID from Job where Job = '{0}'", wc);

            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                this.jID = (int)retVal;
                return (int)retVal;
            }
            else
            {
                return -1;
            }
        }

        public DataTable SelectTbl(string wc)
        {
            string sqlCmd = string.Format("select * from Job where Job = '{0}'", wc);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            return retVal;
        }

        public bool InsertUpdateTbl()
        {
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

        private bool UpdateTbl()
        {
            string sqlCmd = string.Format("update Job set InProcessCt = {0} where Job = '{1}'", this.InProcessCt, this.jobNum);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();

            return true;
        }

        private bool InsertTbl()
        {
            bool ret = false;

            string sqlCmd = string.Format("insert into Job (Job, PartNumber, PartVersion, CreateUser, CreateComputer, InProcessCt) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                                           this.jobNum, this.PartNumber, this.PartVersion, Environment.UserName, Environment.MachineName, this.InProcessCt.ToString() );
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();

            return ret;
        }

        private bool CheckRecordExist()
        {
            bool ret = false;

            string sqlCmd = string.Format("select InProcessCt from Job where Job = '{0}'", this.jobNum);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = true;
                // Get the existing inproccnt value and add the new count
                this.InProcessCt += Convert.ToInt32(retVal);
            }

            return ret;
        }

    } // class
}
