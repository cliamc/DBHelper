using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class MfgDataPart
    {
        private int pID;
        private int jID;
        private string SerialNumber;

        private bool TopLevel;
        private bool Associated;
        private DateTime? AssociatedTime;

        private DateTime? createtime = DateTime.Now;

        private AccessSQL dbAccess = new AccessSQL();

        public MfgDataPart()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public MfgDataPart(int jobID)
        {
            this.jID = jobID;

            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public MfgDataPart(string SN)
        {
            this.SerialNumber = SN;

            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public int GetPID(string sn)
        {
            string sqlCmd = string.Format("select pID from Part where SerialNumber = '{0}'", sn);

            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();

            if (retVal != null)
            {
                this.pID = (int)retVal;
                return this.pID;
            }
            else
            {
                return -1;
            }
        }

        public DataTable SelectTbl()
        {
            string sqlCmd = string.Format("select * from Part where jID = {0}", this.jID);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            return dt;
        }

        public List<string> GetJobSerialNumbers(string job)
        {
            List<string> sns = new List<string>();

            string sqlCmd = string.Format("select p.SerialNumber from Part p join Job j on p.jID = j.jID where j.Job = '{0}' order by p.SerialNumber", job);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    string sn = dRow[0].ToString();
                    if (!string.IsNullOrEmpty(sn))
                    {
                        sns.Add(sn);
                    }
                }
            }
            return sns;
        }

        public void UpdateAssocFlag()
        {
            string sqlCmd = string.Format("update Part set TopLevel = 1, Associated = 1, AssociatedTime = '{0}' where SerialNumber = '{1}'", DateTime.Now, this.SerialNumber);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();
        }

        public bool InsertSerialRecord(string[] sNumbers)
        {
            bool ret = false;

            for (int i = 0; i < sNumbers.Count(); i++)
            {
                try
                {
                    if (!CheckRecordExist(sNumbers[i]))
                    {
                        string sqlCmd = string.Format("insert into Part (jID, SerialNumber, CreateUser, CreateComputer, TopLevel, Associated ) values ('{0}', '{1}', '{2}', '{3}', {4}, {5})",
                                                       this.jID, sNumbers[i], Environment.UserName, Environment.MachineName, 0, 0);
                        dbAccess.SetQueryCmd(sqlCmd);
                        dbAccess.RunSQLcmd();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return ret;
        }

        private bool CheckRecordExist(string sn)
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from Part where SerialNumber = '{0}'", sn);
            dbAccess.SetQueryCmd(sqlCmd);
            object retRec = dbAccess.GetASingleValue();
            if (retRec != null)
                ret = true;

            return ret;
        }

    } // class
}
