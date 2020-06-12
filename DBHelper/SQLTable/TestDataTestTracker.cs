using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public struct TestRecord
    {
        public DateTime DateTime;
        public int ErrorCode;
    }

    public class TestDataTestTracker
    {
        private string SerialNumber = "";
        private int Operation = 0;

        public List<TestRecord> TestRecords = new List<TestRecord>();

        private AccessSQL dbAccess = new AccessSQL();

        public TestDataTestTracker()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrTestData());
        }

        public TestDataTestTracker(string sn)
        {
            this.SerialNumber = sn;

            dbAccess.SetConnStr(DBConnectionStr.ConnStrTestData());

            SelectTblAllOps();
        }


        public TestDataTestTracker(string sn, int op)
        {
            this.SerialNumber = sn;
            this.Operation = op;
            dbAccess.SetConnStr(DBConnectionStr.ConnStrTestData());

            SelectTbl();
        }

        private void SelectTbl()
        {
            string sqlCmd = string.Format("select TestDate, TestErrorCode from TestTracker where Baseplate = '{0}' and Operation = {1} order by TestDate desc", this.SerialNumber, this.Operation);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            if (dt.Rows.Count > 0)
            {
                foreach(DataRow dRow in dt.Rows)
                {
                    TestRecord tr = new TestRecord();
                    tr.DateTime = (DateTime)dRow["TestDate"];
                    tr.ErrorCode = (int)dRow["TestErrorCode"];

                    this.TestRecords.Add(tr);
                }
            }
        }

        private void SelectTblAllOps()
        {
            string sqlCmd = string.Format("select TestDate, TestErrorCode from TestTracker where Baseplate = '{0}' order by Operation, TestDate", this.SerialNumber);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    TestRecord tr = new TestRecord();
                    tr.DateTime = (DateTime)dRow["TestDate"];
                    tr.ErrorCode = (int)dRow["TestErrorCode"];

                    this.TestRecords.Add(tr);
                }
            }
        }

        public bool GetTestOutcome()
        {
            bool ret = false;

            TestRecord tr = new TestRecord();

            if (this.TestRecords.Count > 0)
            {
                //tr = this.TestRecords[0];                       // the first element of the list, index starts from 0
                tr = this.TestRecords.FirstOrDefault();
                if (tr.ErrorCode == 0)
                    ret = true;
            }
            return ret;
        }

    } // class
}
