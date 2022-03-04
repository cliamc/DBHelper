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
        public string PartNumber = "";                                                 // used if insert
        public string PartVersion = "";

        private DateTime CreateDate = DateTime.Today;                              // used if insert

        public int Quantity = 0;
        private int InProcessCt = 0;                                                       // used if insert

        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataJob()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public MfgDataJob(string wc, string pn, string ver, int ct, int qty)
        {
            this.jobNum = wc;
            this.PartNumber = pn;
            this.PartVersion = ver;

            // How to use Quantity is TBD; It is used in handling Scrap process, 8/26/2021
            this.InProcessCt = ct;
            this.Quantity = qty;

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

        public int GetQuantity(string wc)
        {
            string sqlCmd = string.Format("select Quantity from Job where Job = '{0}'", wc);

            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                this.Quantity = (int)retVal;
                return (int)retVal;
            }
            else
            {
                return 0;
            }
        }


        public string GetJobWithPart(string pn, string ver)
        {
            string ret = "";

            string sqlCmd = string.Format("select Job from Job where PartNumber = '{0}' and PartVersion = '{1}'", pn, ver);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                ret = retVal.ToString();
            }
           
            return ret;
        }

        public bool GetPNandRev(string job, ref string pn, ref string rev)
        {
            bool ret = false;

            string sqlCmd = string.Format("select PartNumber, PartVersion from Job where Job = '{0}'", job);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();
            if (retVal.Rows.Count != 0)
            {
                pn = retVal.Rows[0]["PartNumber"].ToString();
                rev = retVal.Rows[0]["PartVersion"].ToString();
                ret = true;
            }

            return ret;
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

        public void UpdatePartVersion()
        {
            try
            {
                string tVer = GetPartVersion();
                if (tVer == "") tVer = "0.0";
                if ( !string.IsNullOrEmpty(this.PartVersion) && IsNumeric(this.PartVersion) && VersionLarger(this.PartVersion, tVer))
                {
                    string sqlCmd = string.Format("update Job set PartVersion = {0} where Job = '{1}'", this.PartVersion, this.jobNum);
                    dbAccess.SetQueryCmd(sqlCmd);
                    dbAccess.RunSQLcmd();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GetRoHS(string wc)
        {
            bool ret = false;

            string sqlCmd = string.Format("select RoHScompliant from Job where Job = '{0}'", wc);
            dbAccess.SetQueryCmd(sqlCmd);
            object dt = dbAccess.GetASingleValue();

            if (dt != null)
            {
                ret = (bool)dt;
            }

            return ret;
        }

        // For CheckROHS application; Charles Li, 3/14/2019
        public void UpdateROHS(string jj, bool rs)
        {
            bool chked = true;
            string sqlCmd = string.Format("update Job set RoHScompliant = '{0}', RoHSchecked = '{1}' where Job = '{2}'", rs, chked, jj);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();
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

            string sqlCmd = string.Format("insert into Job (Job, PartNumber, PartVersion, CreateUser, CreateComputer, Quantity, InProcessCt) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                           this.jobNum, this.PartNumber, this.PartVersion, Environment.UserName, Environment.MachineName, this.Quantity.ToString(), this.InProcessCt.ToString() );
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

        private string GetPartVersion()
        {
            string ret = "";
            string sqlCmd = string.Format("select PartVersion from Job where Job = '{0}'", this.jobNum);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = (string)retVal;
            }

            return ret;
        }

        private bool VersionLarger(string partNumNew, string partNumOld)
        {
            float newVer = Convert.ToSingle(partNumNew);
            float oldVer = Convert.ToSingle(partNumOld);
            if (newVer > oldVer)
                return true;
            else
                return false;
        }

        private bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

    } // class
}
