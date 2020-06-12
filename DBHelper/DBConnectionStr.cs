using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    public static class DBConnectionStr
    {
        const string ConnectionStrMfgData = "Data Source=amc-sql01;Initial Catalog=AMC_MfgData;Persist Security Info=True;User ID=autest;Password=autest1234";
        public const string ConnectionStrMfgDataDev = "Data Source=amc-sql01;Initial Catalog=AMC_MfgDataDev;Persist Security Info=True;User ID=autest;Password=autest1234";

        private static string ConnectionStrTestData = "Data Source=amc-sql01;Initial Catalog=AMC_TestData;Persist Security Info=True;User ID=autest;Password=autest1234";

        private static string ConnectionStrProdmanData = "Data Source=amc-sql01;Initial Catalog=AMC_Prodman;Persist Security Info=True;User ID=cliservice;Password=n3wCopper24";

        public const string ConnectionStrPLMDataLoad = @"Data Source=amc-plm-01;Initial Catalog=DataLoad;Persist Security Info=True;User ID=plmload;Password='=reeWing28'";
        /*** harlequin - prodmansql / pr0dman5ux ***/

        public const string ConnStrEpicorProd = "Data Source=bitis;Initial Catalog=epicor905;Persist Security Info=True;User ID=e9prog;Password=e9prog";
        const string ConnStrEpicorTest = "Data Source=bitis;Initial Catalog=epicortest905;Persist Security Info=True;User ID=e9prog;Password=e9prog";

        /******* Stop developing code for FoxPro *******/
        private static string OLEFoxProProd = @"Data Source=\\BOA\Production\Prodman.vfp\DATABASE\{0}.dbf;Provider=VFPOLEDB.1;";
        private static string OLEFoxProDev = @"Data Source=\\BOA\Production\Prodman.dev\DATABASE\{0}.dbf;Provider=VFPOLEDB.1;";

        private static string ODBCFoxProProd = @"Driver={Microsoft Visual FoxPro Driver};UID=;PWD=;SourceDB=\\BOA\Production\Prodman.vfp\DATABASE\{0}.dbf;SourceType=DBF;Exclusive=No;BackgroundFetch=Yes;Collate=Machine;Null=Yes;Deleted=Yes;";
        private static string ODBCFoxProDev1 = @"Driver={Microsoft Visual FoxPro Driver};UID=;PWD=;SourceDB=\\BOA\Production\Prodman.Dev\DATABASE\";
        private static string ODBCFoxProDev3 = ".DBF;SourceType=DBF;Exclusive=No;BackgroundFetch=Yes;Collate=Machine;Null=Yes;Deleted=Yes;";

        private static string testStr = @"Driver={Microsoft dBASE Driver (*.dbf)};DriverID=277;Dbq=\\BOA\Production\Prodman.vfp\DATABASE\amplifs.DBC;";
        /***********************************************/

        public static string ConnStrMfgData()
        {
#if DEBUG
            return ConnectionStrMfgData;

            //return ConnectionStrMfgDataDev;
#else
            return ConnectionStrMfgData;
#endif
        }

        public static string ConnStrTestData()
        {
            return ConnectionStrTestData;
        }

        public static string ConnStrProdmanData()
        {
            return ConnectionStrProdmanData;
        }

        public static string SQLEpicorConnStr()
        {
#if DEBUG
            return ConnStrEpicorProd;                           // For AssociateSubassembly test on 1/14/2020
            //return ConnStrEpicorTest;
#else
            return ConnStrEpicorProd;
#endif
        }

        /*-------*/

        // Do not develop new code for FoxPro
        public static string FoxProODBCConnStr(string TableName)
        {
            string tmp;
#if DEBUG
            //tmp = String.Format(ODBCFoxProDev, TableName);
            //tmp = ODBCFoxProDev1 + TableName + ODBCFoxProDev3;
            tmp = testStr;
#else
            tmp = String.Format(ODBCFoxProProd, TableName);
#endif
            return tmp;
        }

        // VFPOLEDB.1 provider is not registered on the local machine!
        public static string FoxProOLEConnStr(string TableName)
        {
            string tmp;
#if DEBUG
            tmp = string.Format(OLEFoxProDev, TableName);
#else
            tmp = string.Format(OLEFoxProProd, TableName);
#endif
            return tmp;
        }

    } // class
}
