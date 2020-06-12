using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBHelper.SQLDataLoadTbl
{
    public struct DesignNoteData
    {
        public string PartNumber;
        public string Revision;
        public string DocumentType;
        public string DocumentTitle;
        public string VaultName;
        public string NoteFileFullPath;
    }

    public class PLMDataLoadEngineerDesignNote
    {
        private AccessSQL dbAccess = new AccessSQL();

        // Class constructor
        public PLMDataLoadEngineerDesignNote()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertARecord(DesignNoteData dnd)
        {
            bool ret = false;

            try
            {
                if (!CheckRecordExist(dnd.DocumentTitle))
                {
                    string sqlCmd = string.Format("insert into EngineerDesignNote (PartNumber, Revision, DocumentType, DocumentTitle, VaultName, NoteFileFullPath, IsVaulted)"
                                                + " values (@PartNumber, @Revision, @DocumentType, @DocumentTitle, @VaultName, @NoteFileFullPath, '1')");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@PartNumber";
                    sp0.Value = dnd.PartNumber;
                    lsp.Add(sp0);
                    SqlParameter sp1 = new SqlParameter();
                    sp1.ParameterName = "@Revision";
                    sp1.Value = dnd.Revision;
                    lsp.Add(sp1);
                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@DocumentType";
                    sp2.Value = dnd.DocumentType;
                    lsp.Add(sp2);
                    SqlParameter sp3 = new SqlParameter();
                    sp3.ParameterName = "@DocumentTitle";
                    sp3.Value = dnd.DocumentTitle;
                    lsp.Add(sp3);
                    SqlParameter sp4 = new SqlParameter();
                    sp4.ParameterName = "@VaultName";
                    sp4.Value = dnd.VaultName;
                    lsp.Add(sp4);
                    SqlParameter sp5 = new SqlParameter();
                    sp5.ParameterName = "@NoteFileFullPath";
                    sp5.Value = dnd.NoteFileFullPath;
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

        private bool CheckRecordExist(string doctt)
        {
            bool ret = false;

            string sqlCmd = string.Format("select PartNumber from EngineerDesignNote where DocumentTitle = '{0}'", doctt);
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
