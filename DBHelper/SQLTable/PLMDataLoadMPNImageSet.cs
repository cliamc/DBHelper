using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLTable
{
    public struct ImageSetSql
    {
        public string AMCPartNumber;
        public string MfrPartNumber;
        public string SourceName;
        public string LargeURL;
        public byte[] liBA;
        public string MediumURL;
        public byte[] miBA;
        public string SmallURL;
        public byte[] siBA;
        public string SwatchURL;
        public byte[] wiBA;
    }

    public class PLMDataLoadMPNImageSet
    {
        private AccessSQL dbAccess = new AccessSQL();

        public PLMDataLoadMPNImageSet()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertTbl(ImageSetSql er)
        {
            bool ret = false;

            try
            {
                //if (!CheckRecordExist(er.AMCPartNumber, er.MfrPartNumber))
                if (true)
                {
                    string sqlCmd = string.Format("insert into MPNImageSet (RequestMPN, ResultMPN, SourceName, LargeURL, largeImg, MediumURL, mediumImg, SmallURL, smallImg, SwatchURL, swatchImg)"
                                                + " values (@AMCPartNumber, @MfrPartNumber, @SourceName, @LargeURL, @largeImg, @MediumURL, @mediumImg, @SmallURL, @smallImg, @SwatchURL, @swatchImg)");
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
                    sp2.ParameterName = "@SourceName";
                    sp2.Value = er.SourceName;
                    lsp.Add(sp2);

                    SqlParameter sp3 = new SqlParameter();
                    sp3.ParameterName = "@LargeURL";
                    sp3.Value = er.LargeURL;
                    lsp.Add(sp3);
                    SqlParameter sp4 = new SqlParameter();
                    sp4.ParameterName = "@largeImg";
                    sp4.Value = er.liBA;
                    lsp.Add(sp4);

                    SqlParameter sp5 = new SqlParameter();
                    sp5.ParameterName = "@MediumURL";
                    sp5.Value = er.MediumURL;
                    lsp.Add(sp5);
                    SqlParameter sp6 = new SqlParameter();
                    sp6.ParameterName = "@mediumImg";
                    sp6.Value = er.miBA;
                    lsp.Add(sp6);

                    SqlParameter sp7 = new SqlParameter();
                    sp7.ParameterName = "@SmallURL";
                    sp7.Value = er.SmallURL;
                    lsp.Add(sp7);
                    SqlParameter sp8 = new SqlParameter();
                    sp8.ParameterName = "@smallImg";
                    sp8.Value = er.siBA;
                    lsp.Add(sp8);

                    SqlParameter sp9 = new SqlParameter();
                    sp9.ParameterName = "@SwatchURL";
                    sp9.Value = er.SwatchURL;
                    lsp.Add(sp9);
                    SqlParameter sp10 = new SqlParameter();
                    sp10.ParameterName = "@swatchImg";
                    sp10.Value = er.wiBA;
                    lsp.Add(sp10);

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

            string sqlCmd = string.Format("select * from PLMDataLoadMPNImageSet where RequestMPN = '{0}' and ResultMPN = '{1}'", amc, mpn);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = true;
            }

            return ret;
        }

        public DataTable GetMPNImages(string mpn)
        {
            DataTable dt = new DataTable();

            try
            {
                string sqlCmd = string.Format("select * from MPNImageSet where RequestMPN = '{0}'", mpn);
                dbAccess.SetQueryCmd(sqlCmd);

                dt = dbAccess.ReadDbData();

            }
            catch (Exception)
            {
                throw;
            }

            return dt;
        }

    } // class
}
