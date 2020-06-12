using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.SQLDataLoadTbl
{
    public struct DataSheetData
    {
        public string PartNumber;
        public string DocumentType;
        public string DocumentName;
        public string DocumentTitle;
        public string VaultName;
        public string DocumentPath;
    }

    public class PLMDataLoadDatasheets
    {
        private AccessSQL dbAccess = new AccessSQL();

        // Class constructor
        public PLMDataLoadDatasheets()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertARecord(DataSheetData ddd)
        {
            bool ret = false;

            try
            {
                if (!CheckRecordExist(ddd.PartNumber, ddd.DocumentPath))
                {
                    string sqlCmd = string.Format("insert into Datasheets (PartNumber, DocumentType, DocumentName, DocumentTitle, VaultName, DocumentPath, IsVaulted)"
                                                + " values (@PartNumber, @DocumentType, @DocumentName, @DocumentTitle, @VaultName, @DocumentPath, '1')");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@PartNumber";
                    sp0.Value = ddd.PartNumber;
                    lsp.Add(sp0);
                    SqlParameter sp1 = new SqlParameter();
                    sp1.ParameterName = "@DocumentType";
                    sp1.Value = ddd.DocumentType;
                    lsp.Add(sp1);
                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@DocumentName";
                    sp2.Value = ddd.DocumentName;
                    lsp.Add(sp2);
                    SqlParameter sp3 = new SqlParameter();
                    sp3.ParameterName = "@DocumentTitle";
                    sp3.Value = ddd.DocumentTitle;
                    lsp.Add(sp3);
                    SqlParameter sp4 = new SqlParameter();
                    sp4.ParameterName = "@VaultName";
                    sp4.Value = ddd.VaultName;
                    lsp.Add(sp4);
                    SqlParameter sp5 = new SqlParameter();
                    sp5.ParameterName = "@DocumentPath";
                    sp5.Value = ddd.DocumentPath;
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

        private bool CheckRecordExist(string PN, string path)
        {
            bool ret = false;

            string sqlCmd = string.Format("select PartNumber from Datasheets where PartNumber = '{0}' and DocumentPath = '{1}'", PN, path);
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
