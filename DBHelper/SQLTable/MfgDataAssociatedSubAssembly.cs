using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    //public struct MarriedRecord
    //{
    //    public string SubSerialNumber;
    //    public string SubPartNumber;
    //    public string SubPartVersion;
    //    public DateTime AssociatedTime;
    //}

    public class MfgDataAssociatedSubAssembly
    {
        private AccessSQL dbAccess = new AccessSQL();

        //private List<MarriedRecord> MSubs = new List<MarriedRecord>();

        private int pID;
        private string SerialNumber;
        private string partNumber;
        private string PartVersion;

        private string UserName = Environment.UserName;
        private string ComputerName = Environment.MachineName;

        private DateTime ModifyTime = DateTime.Now;

        public MfgDataAssociatedSubAssembly()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public MfgDataAssociatedSubAssembly(int partID)
        {
            this.pID = partID;

            dbAccess.SetConnStr(DBConnectionStr.ConnStrMfgData());
        }

        public DataTable SelectTbl()
        {
            string sqlCmd = string.Format("select * from AssociatedSubAssembly where pID = {0} order by AssociatedTime", this.pID);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            return dt;
        }

        public DataTable SelectTblforJob(string jobNum)
        {
            string sqlCmd = string.Format("select a.SerialNumber from AssociatedSubAssembly a join Part p on a.pID = p.pID where p.SerialNumber like '{0}-%'", jobNum);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            return dt;
        }

        public DataTable GetTopLevelMarryRecords(string tlsn)                           // Used in RemoveAssociationForm
        {
            string sqlCmd = string.Format("select a.asID, a.pID, a.SerialNumber, a.PartNumber, a.PartVersion, a.AssociatedTime, a.AssociateUser, a.AssociateComputer " +
                                          "from AssociatedSubAssembly a join Part p on a.pID = p.pID where p.SerialNumber like '{0}' order by a.AssociatedTime", tlsn);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            return dt;
        }

        public DataTable GetJoblMarriedRecords(string job)                           // Used in ViewAssociationForm
        {
            string sqlCmd = string.Format("select p.SerialNumber, a.SerialNumber, a.PartNumber, a.AssociatedTime, a.AssociateUser, a.AssociateComputer from AssociatedSubAssembly a" +
                                          " join Part p on a.pID = p.pID where p.SerialNumber like '{0}%' order by p.SerialNumber, a.PartNumber", job);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            return dt;
        }

        public void InsertRecord(string subSN, string subPN, string subVer)
        {
            string sqlCmd = string.Format("insert into AssociatedSubAssembly (pID, SerialNumber, PartNumber, PartVersion, AssociateUser, AssociateComputer)" +
                                       "values ({0}, '{1}', '{2}', '{3}', '{4}', '{5}')", this.pID, subSN, subPN, subVer, this.UserName, this.ComputerName);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();
        }

        public void UpdateRecord(string subSN, string subVer, string curSN)
        {
            // Update the subassembly serial number to the new value
            string tStampe = DateTime.Now.ToString();
            string sqlCmd = string.Format("update AssociatedSubAssembly set SerialNumber = '{0}', PartVersion = '{1}', ModifyTime = '{2}', ModifyUser = '{3}', ModifyComputer = '{4}' " +
                                       "where SerialNumber = '{5}'", subSN, subVer, tStampe, this.UserName, this.ComputerName, curSN);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();
        }

        // Deprecated! 2/22/2019
        public void InsertUpdateRecord(string subSN, string subPN, string subVer)
        {
            string sqlCmd = "";
            if (!CheckRecordExist(subPN)) {
                sqlCmd = string.Format("insert into AssociatedSubAssembly (pID, SerialNumber, PartNumber, PartVersion, AssociateUser, AssociateComputer)" +
                                       "values ({0}, '{1}', '{2}', '{3}', '{4}', '{5}')", this.pID, subSN, subPN, subVer, this.UserName, this.ComputerName);
            }
            else
            {
                // Update the subassembly serial number to the new value
                string tStampe = DateTime.Now.ToString();
                sqlCmd = string.Format("update AssociatedSubAssembly set SerialNumber = '{0}', PartVersion = '{1}', ModifyTime = '{2}', ModifyUser = '{3}', ModifyComputer = '{4}' " +
                                       "where pID = {5} and PartNumber = '{6}'", subSN, subVer, tStampe, this.UserName, this.ComputerName, this.pID, subPN);
            }
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();
        }

        // Deprecated! 2/22/2019
        private bool CheckRecordExist(string subPN)
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from AssociatedSubAssembly where pID = {0} and PartNumber = '{1}'", this.pID, subPN);
            dbAccess.SetQueryCmd(sqlCmd);
            object retRec = dbAccess.GetASingleValue();
            if (retRec != null)
                ret = true;

            return ret;
        }

        // The subassembly component's serial number remains the same but the version number is changed
        public void UpdateVersion(string Ver, string subSN)
        {
            DateTime tStampe = DateTime.Now;
            string  sqlCmd = string.Format("update AssociatedSubAssembly set PartVersion = '{0}', ModifyTime = '{1}', ModifyUser = '{2}', ModifyComputer = '{3}' " +
                                           "where SerialNumber = '{4}'", Ver, tStampe, this.UserName, this.ComputerName, subSN);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();
        }

        public bool CheckSubSNused(string subSN)
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from AssociatedSubAssembly where SerialNumber = '{0}'", subSN);
            dbAccess.SetQueryCmd(sqlCmd);
            object retRec = dbAccess.GetASingleValue();
            if (retRec != null)
                ret = true;

            return ret;
        }

        public string CheckSubSNusedTL(string subSN)
        {
            string ret = "";

            string sqlCmd = string.Format("select p.SerialNumber from AssociatedSubAssembly a JOIN Part p on a.pID = p.pID where a.SerialNumber = '{0}'", subSN);
            dbAccess.SetQueryCmd(sqlCmd);
            object retRec = dbAccess.GetASingleValue();
            if (retRec != null)
                ret = (string)retRec;

            return ret;
        }



        /* Unmarry - Undo Association operation method, 8/25/2020
         */
        public void DeleteRecordUnmarry(string subSN)
        {
            string sqlCmd = string.Format("delete from AssociatedSubAssembly where SerialNumber = '{0}'", subSN);
            dbAccess.SetQueryCmd(sqlCmd);
            dbAccess.RunSQLcmd();
        }

    } // class
}
