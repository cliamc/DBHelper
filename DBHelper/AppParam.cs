using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBHelper.SQLTable;

namespace DBHelper
{
    public static class AppParam
    {
        public static string LabelConfigFile;
        public static string TestPathFolder;
        public static string LabelRule;
        public static string ServerName;
        public static string TextDropDirectory;
        public static string BtwFileDirectory;
        public static string DBServerName;
        public static string DatabaseName;
        public static string DatabaseUserID;
        public static string DatabasePW;
        public static string BtwFilePath;
        public static string Smt;
        public static string InProcess;
        public static string BasePlate;
        public static List<string> Final = new List<string>();
        public static bool bProdTrace = false;
        public static List<string> rma = new List<string>();

        public static void SetValue()
        {
            //XLiUtilDb xliDb = new XLiUtilDb();
            //xliDb.SetConnStr(SQLConnectionStr.AppSQLConnStr());
            //xliDb.SetQueryCmd(@"select ParamName, ParamValue from ApplicationParam where Application = 'AMCLabelPrint'");

            try
            {
                //DataTable dttb = xliDb.ReadDbData();

                MfgDataApplicationParam mdap = new MfgDataApplicationParam("AMCLabelPrint");
                DataTable dttb = mdap.SelectTbl();

                foreach (DataRow dRow in dttb.Rows)
                {
                    string pn = dRow["ParamName"].ToString();
                    if (pn == "LabelConfigFile")
                        LabelConfigFile = dRow["ParamValue"].ToString();
                    else if (pn.Equals("TestPathFolder"))
                        TestPathFolder = dRow["ParamValue"].ToString();
                    else if (pn.Equals("LabelRule"))
                        LabelRule = dRow["ParamValue"].ToString();
                    else if (pn.Equals("ServerName"))
                        ServerName = dRow["ParamValue"].ToString();
                    else if (pn.Equals("TextDropDirectory"))
                        TextDropDirectory = dRow["ParamValue"].ToString();
                    else if (pn.Equals("BtwFileDirectory"))
                        BtwFileDirectory = dRow["ParamValue"].ToString();
                    else if (pn.Equals("DBServerName"))
                        DBServerName = dRow["ParamValue"].ToString();
                    else if (pn.Equals("DatabaseName"))
                        DatabaseName = dRow["ParamValue"].ToString();
                    else if (pn.Equals("DatabaseUserID"))
                        DatabaseUserID = dRow["ParamValue"].ToString();
                    else if (pn.Equals("DatabasePW"))
                        DatabasePW = dRow["ParamValue"].ToString();
                    else if (pn.Equals("BtwFilePath"))
                        BtwFilePath = dRow["ParamValue"].ToString();
                    else if (pn.Equals("Smt"))
                        Smt = dRow["ParamValue"].ToString();
                    else if (pn.Equals("InProcess"))
                        InProcess = dRow["ParamValue"].ToString();
                    else if (pn.Equals("BasePlate"))
                        BasePlate = dRow["ParamValue"].ToString();
                    else if (pn.Equals("Final"))
                        //Final = dRow["ParamValue"].ToString();
                        Final = (dRow["ParamValue"].ToString()).Split(',').ToList();
                    else if (pn.Equals("Trace"))
                    {
                        string tmp = dRow["ParamValue"].ToString();
                        if (tmp.Equals("True"))
                            bProdTrace = true;
                    }
                    else if (pn.Equals("RMA"))
                        //Final = dRow["ParamValue"].ToString();
                        rma = (dRow["ParamValue"].ToString()).Split(',').ToList();
                }
            }
            catch (Exception)
            {
                throw;                          // to be caught and treated at the calling place
            }

#if DEBUG
            TextDropDirectory = @"btwFiles\zLPtest";
#endif

        }

    } // static class
}
