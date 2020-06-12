using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public struct SpecAttributeSql
    {
        public string AMCPartNumber;
        public string MfrPartNumber;
        public string AttributeName;
        public string SourceName;
        public string DisplayValue;
        public string Value;
        public string UofM;
    }

    public class PLMDataLoadMPNSpecs
    {
        private AccessSQL dbAccess = new AccessSQL();

        public PLMDataLoadMPNSpecs()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertTbl(SpecAttributeSql er)
        {
            bool ret = false;

            try
            {
                //if (!CheckRecordExist(er.AMCPartNumber, er.MfrPartNumber))
                if (true)
                {
                    string sqlCmd = string.Format("insert into MPNSpecs (RequestMPN, ResultMPN, AttributeName, SourceName, DisplayValue, Value, UofM)"
                                                + " values (@AMCPartNumber, @MfrPartNumber, @AttributeName, @SourceName, @DisplayValue, @Value, @UofM)");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@AMCPartNumber";
                    sp0.Value = er.AMCPartNumber;
                    lsp.Add(sp0);
                    SqlParameter sp1 = new SqlParameter();
                    sp1.ParameterName = "@MfrPartNumber";
                    sp1.Value = er.MfrPartNumber;
                    lsp.Add(sp1);
                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@AttributeName";
                    sp2.Value = er.AttributeName;
                    lsp.Add(sp2);
                    SqlParameter sp3 = new SqlParameter();
                    sp3.ParameterName = "@SourceName";
                    sp3.Value = er.SourceName;
                    lsp.Add(sp3);

                    SqlParameter sp4 = new SqlParameter();
                    sp4.ParameterName = "@DisplayValue";
                    sp4.Value = er.DisplayValue;
                    lsp.Add(sp4);
                    SqlParameter sp5 = new SqlParameter();
                    sp5.ParameterName = "@Value";
                    sp5.Value = er.Value;
                    lsp.Add(sp5);
                    SqlParameter sp6 = new SqlParameter();
                    sp6.ParameterName = "@UofM";
                    sp6.Value = er.UofM;
                    lsp.Add(sp6);

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

            string sqlCmd = string.Format("select * from PLMDataLoadMPNSpecs where RequestMPN = '{0}' and ResultMPN = '{1}'", amc, mpn);
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
