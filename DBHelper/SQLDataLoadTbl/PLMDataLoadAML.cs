using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBHelper.SQLTable
{
    public struct AMLdata
    {
        public string AMCPartNumber;
        public string PartStatus;
        public string Description;
        public string MfrName;
        public string MfrPartNumber;
    }

    public class PLMDataLoadAML
    {
        private AccessSQL dbAccess = new AccessSQL();

        public PLMDataLoadAML()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public DataTable SelectTbl()
        {
            string sqlCmd = string.Format("select * from AML");
            dbAccess.SetQueryCmd(sqlCmd);
            DataTable retVal = dbAccess.ReadDbData();

            return retVal;
        }

        public bool InsertTbl(AMLdata er)
        {
            bool ret = false;

            try
            {
                //if (!CheckRecordExist(er.MfrPartNumber))
                if (true)
                {
                    string sqlCmd = string.Format("insert into AML (AMCPartNumber, PartStatus, Description, MfrName, MfrPartNumber)"
                                                + " values (@AMCPartNumber, @PartStatus, @Description, @MfrName, @MfrPartNumber)");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();

                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@AMCPartNumber";
                    sp0.Value = er.AMCPartNumber;
                    lsp.Add(sp0);

                    SqlParameter sp1 = new SqlParameter();
                    sp1.ParameterName = "@PartStatus";
                    sp1.Value = er.PartStatus;
                    lsp.Add(sp1);

                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@Description";
                    sp2.Value = er.Description;
                    lsp.Add(sp2);
                    SqlParameter sp4 = new SqlParameter();
                    sp4.ParameterName = "@MfrName";
                    sp4.Value = er.MfrName;
                    lsp.Add(sp4);
                    SqlParameter sp5 = new SqlParameter();
                    sp5.ParameterName = "@MfrPartNumber";
                    sp5.Value = er.MfrPartNumber;
                    lsp.Add(sp5);

                    dbAccess.RunSQLcmdParam(lsp);

                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

        private bool CheckRecordExist(string mpn)
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from AML where MfrPartNumber = '{0}'", mpn);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = true;
            }

            return ret;
        }

    } // class
}
