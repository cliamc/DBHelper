using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBHelper.SQLDataLoadTbl
{
    public struct DesignDocData
    {
        public string PartNumber;
        public string Revision;
        public string DocumentType;
        public string DocumentTitle;
        public string VaultName;
        public string DocFullPath;
    }

    public class PLMDataLoadEngineerDesignDoc
    {
        private AccessSQL dbAccess = new AccessSQL();

        // Class constructor
        public PLMDataLoadEngineerDesignDoc()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertARecord(DesignDocData ddd)
        {
            bool ret = false;

            try
            {
                if (!CheckRecordExist(ddd.PartNumber, ddd.Revision, ddd.DocFullPath))
                {
                    string sqlCmd = string.Format("insert into EngineerDesignDoc (PartNumber, Revision, DocumentType, DocumentTitle, VaultName, DocFullPath, IsVaulted)"
                                                + " values (@PartNumber, @Revision, @DocumentType, @DocumentTitle, @VaultName, @DocFullPath, '1')");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@PartNumber";
                    sp0.Value = ddd.PartNumber;
                    lsp.Add(sp0);
                    SqlParameter sp1 = new SqlParameter();
                    sp1.ParameterName = "@Revision";
                    sp1.Value = ddd.Revision;
                    lsp.Add(sp1);
                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@DocumentType";
                    sp2.Value = ddd.DocumentType;
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
                    sp5.ParameterName = "@DocFullPath";
                    sp5.Value = ddd.DocFullPath;
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

        public bool CheckRecordExist(string PN, string Rev, string path)
        {
            bool ret = false;

            string sqlCmd = string.Format("select PartNumber from EngineerDesignDoc where PartNumber = '{0}' and Revision = '{1}' and DocFullPath = '{2}'", PN, Rev, path);
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
