using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public struct OfferSql
    {
        public string AMCPartNumber;
        public string MfrPartNumber;
        public DateTime LastUpdated;
        public string Seller;
    }

    public class PLMDataLoadMPNOffer
    {
        private AccessSQL dbAccess = new AccessSQL();

        public int OfferID;

        public PLMDataLoadMPNOffer()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertTbl(OfferSql er)
        {
            bool ret = false;

            try
            {
                //if (!CheckRecordExist(er.AMCPartNumber, er.MfrPartNumber))
                if (true)
                {
                    string sqlCmd = string.Format("insert into MPNOffer (RequestMPN, ResultMPN, LastUpdated, Seller)"
                                                + " values (@AMCPartNumber, @MfrPartNumber, @LastUpdated, @Seller)");
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
                    sp2.ParameterName = "@LastUpdated";
                    sp2.Value = er.LastUpdated;
                    sp2.SqlDbType = SqlDbType.DateTime;
                    lsp.Add(sp2);
                    SqlParameter sp3 = new SqlParameter();
                    sp3.ParameterName = "@Seller";
                    sp3.Value = er.Seller;
                    lsp.Add(sp3);

                    dbAccess.RunSQLcmdParam(lsp);

                    // Get and set the OfferID
                    sqlCmd = string.Format("select MPNofferID from MPNOffer where RequestMPN = '{0}' and ResultMPN = '{1}' and LastUpdated = '{2}' and Seller = '{3}'",
                                            er.AMCPartNumber, er.MfrPartNumber, er.LastUpdated, er.Seller);
                    dbAccess.SetQueryCmd(sqlCmd);
                    object retVal = dbAccess.GetASingleValue();
                    if (retVal != null)
                    {
                        this.OfferID = (int)retVal;
                    }

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

            string sqlCmd = string.Format("select * from PLMDataLoadMPNOffer where RequestMPN = '{0}' and ResultMPN = '{1}'", amc, mpn);
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
