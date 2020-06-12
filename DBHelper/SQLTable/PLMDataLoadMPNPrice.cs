using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public struct OfferPriceSql
    {
        public int MPNofferID;
        public int TierQuantity;
        public decimal Price;
    }

    public class PLMDataLoadMPNPrice
    {
        private AccessSQL dbAccess = new AccessSQL();

        public int OfferID;

        public PLMDataLoadMPNPrice()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertTbl(OfferPriceSql er)
        {
            bool ret = false;

            try
            {
                //if (!CheckRecordExist(er.AMCPartNumber, er.MfrPartNumber))
                if (true)
                {
                    string sqlCmd = string.Format("insert into MPNofferPrice (MPNofferID, TierQuantity, Price)"
                                                + " values (@MPNofferID, @TierQuantity, @Price)");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@MPNofferID";
                    sp0.Value = er.MPNofferID;
                    sp0.SqlDbType = SqlDbType.Int;
                    lsp.Add(sp0);
                    SqlParameter sp1 = new SqlParameter();
                    sp1.ParameterName = "@TierQuantity";
                    sp1.Value = er.TierQuantity;
                    sp1.SqlDbType = SqlDbType.Int;
                    lsp.Add(sp1);
                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@Price";
                    sp2.Value = er.Price;
                    sp2.SqlDbType = SqlDbType.Decimal;
                    lsp.Add(sp2);

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

        private bool CheckRecordExist(int pID, int oID)
        {
            bool ret = false;

            string sqlCmd = string.Format("select * from MPNofferPrice where OfferPriceID = {0} and MPNofferID = {1}", pID, oID);
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
