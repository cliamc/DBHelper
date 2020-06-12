using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public struct DataSheetSql
    {
        public string AMCPartNumber;
        public string MfrPartNumber;
        public string SourceName;
        public string DataSheetURL;
    }

    public class PLMDataLoadMPNDataSheet
    {
        private AccessSQL dbAccess = new AccessSQL();

        public PLMDataLoadMPNDataSheet()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertTbl(DataSheetSql er)
        {
            bool ret = false;

            try
            {
                //if (!CheckRecordExist(er.AMCPartNumber, er.MfrPartNumber))
                if (true)
                {
                    string sqlCmd = string.Format("insert into MPNDataSheet (RequestMPN, ResultMPN, SourceName, DataSheetURL)"
                                                + " values (@AMCPartNumber, @MfrPartNumber, @SourceName, @DataSheetURL)");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@AMCPartNumber";
                    sp0.Value = er.AMCPartNumber;
                    lsp.Add(sp0);
                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@MfrPartNumber";
                    sp2.Value = er.MfrPartNumber;
                    lsp.Add(sp2);
                    SqlParameter sp3 = new SqlParameter();
                    sp3.ParameterName = "@SourceName";
                    sp3.Value = er.SourceName;
                    lsp.Add(sp3);
                    SqlParameter sp5 = new SqlParameter();
                    sp5.ParameterName = "@DataSheetURL";
                    sp5.Value = er.DataSheetURL;
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

        private bool CheckRecordExist(string amc, string mpn)
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from PLMDataLoadMPNDataSheet where RequestMPN = '{0}' and ResultMPN = '{1}'", amc, mpn);
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
