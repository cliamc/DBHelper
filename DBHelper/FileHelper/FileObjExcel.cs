using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace DBHelper.FileHelper
{
    public class FileObjExcel : FileObj
    {
        // private string _fileName;
        // public List<FileLine> fFile = new List<FileLine>();

        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        private DataTable dt = new DataTable();

        public override void ReadContent()
        {
            string sheetName;
            string conStr;

            conStr = string.Format(Excel07ConString, this._fileName);
            // Get the name of the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            } // using end

            this.dt.Clear();

            // Read Data from the First Sheet; and present to the GUI screen
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();
                   }
                }
            } // using end
        }

        public DataTable GetDataTable()
        {
            return this.dt;
        }

    } // class
}
