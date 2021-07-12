using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBHelper.SQLDataLoadTbl
{
    public struct ExceptionRecord
    {
        public string Type;
        public string ExceptionRec;
        public string PartNumber;
        public string Rev;
    }

    public class PLMDataLoadDocNoteExceptionRecord
    {
        private AccessSQL dbAccess = new AccessSQL();

        public PLMDataLoadDocNoteExceptionRecord()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool InsertTbl(ExceptionRecord er)
        {
            bool ret = false;

            try
            {
                string sqlCmd = string.Format("insert into DocExceptionRecord ([Type], [ExceptionRec], PartNumber, Revision) values (@Type, @ExceptionRec, @PartNumber, @Rev)");
                dbAccess.SetQueryCmd(sqlCmd);

                List<SqlParameter> lsp = new List<SqlParameter>();
                SqlParameter sp0 = new SqlParameter();
                sp0.ParameterName = "@Type";
                sp0.Value = er.Type;
                lsp.Add(sp0);
                SqlParameter sp1 = new SqlParameter();
                sp1.ParameterName = "@ExceptionRec";
                sp1.Value = er.ExceptionRec;
                lsp.Add(sp1);
                SqlParameter sp2 = new SqlParameter();
                sp2.ParameterName = "@PartNumber";
                sp2.Value = er.PartNumber;
                lsp.Add(sp2);
                SqlParameter sp3 = new SqlParameter();
                sp3.ParameterName = "@Rev";
                sp3.Value = er.Rev;
                lsp.Add(sp3);

                dbAccess.RunSQLcmdParam(lsp);

                ret = true;
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

    } // class
}
