using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public class TestDataTestOps
    {
        private int pID;
        private string PartNumber;
        private string Version;

        bool RegTest = false;
        bool RegBurn = false;
        bool ManTest = false;

        private AccessSQL dbAccess = new AccessSQL();

        public TestDataTestOps()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnStrTestData());
        }

        public TestDataTestOps(int pID)
        {
            this.pID = pID;
            dbAccess.SetConnStr(DBConnectionStr.ConnStrTestData());
        }

        public TestDataTestOps(string pn, string ver)
        {
            this.PartNumber = pn;
            this.Version = ver;
            dbAccess.SetConnStr(DBConnectionStr.ConnStrTestData());

            SelectTbl();
        }

        private void SelectTbl()
        {
            string sqlCmd = string.Format("select * from TestOps where Model = '{0}' and Version = '{1}'", this.PartNumber, this.Version);
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable dt = dbAccess.ReadDbData();

            if (dt.Rows.Count > 0)
            {
                this.RegTest = (bool)dt.Rows[0]["RegTest"];
                this.RegBurn = (bool)dt.Rows[0]["RegBurn"];
                this.ManTest = (bool)dt.Rows[0]["ManTest"];
            }
        }

        public bool GetRegTest()
        {
            return this.RegTest;
        }

        public bool GetRegBurn()
        {
            return this.RegBurn;
        }

        public bool GetManTest()
        {
            return this.ManTest;
        }

    } // class
}
