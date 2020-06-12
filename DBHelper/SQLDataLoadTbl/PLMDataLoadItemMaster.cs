using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBHelper.SQLTable
{
    public struct ItemRecord
    {
        public string Part;
        public string Version;
        public string Description;
        public string Type;
        public string Category;
        public string Status;
        public string MakeOrPurchase;
        public string BOMUOM;
        public string SalesUOM;
        public string PurchaseUOM;
        public string TotalAvgCost;
        public string ClassCode;
        public string GroupCode;
        public string RoHS;
        public string OperationLocation;

        /* -------  For ItemMasterTryOne ------- */
        //public string Part;
        //public string Version;
        //public string Description;
        //public bool Inactive;
        //public string MakePurch;
        //public string InvUOM;
        //public string SlsUOM;
        //public string PurUOM;
        //public string TotalAvgCost;
        //public string ClassCode;
        //public string GroupCode;
        //public bool Approved;
        //public string RoHS;
        //public string OperationLocation;
    }

    public class PLMDataLoadItemMaster
    {
        private AccessSQL dbAccess = new AccessSQL();

        public PLMDataLoadItemMaster()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        // Old version, need to modify!!! CLi, 2/24/2020 +++++++
        public bool InsertTbl(ItemRecord er)
        {
            bool ret = false;

            try
            {
                //if (!CheckRecordExist(er.Part))
                if (true)
                {
                    string sqlCmd = string.Format("insert into ItemMaster ([AMC Part Number], [Part Version], [Part Description], [Part Type], [Part Category], " +
                                                  "[Part Status in Epicor], [Make or Purchase Part], [BOM UOM], [Sales UOM], [Purchasing UOM], " +
                                                  "[Total Average Cost of Part], [Class Code], [Group Code], [RoHS Compliance], [Operation Location])"
                                                + " values (@Part, @Version, @Description, @Type, @Category, @Status, @MakeOrPurchase, @BOMUOM, @SalesUOM, @PurchaseUOM,"
                                                + " @TotalAvgCost, @ClassCode, @GroupCode, @RoHS, @OperationLocation)");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@Part";
                    sp0.Value = er.Part;
                    lsp.Add(sp0);
                    SqlParameter sp1 = new SqlParameter();
                    sp1.ParameterName = "@Version";
                    sp1.Value = er.Version;
                    lsp.Add(sp1);
                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@Description";
                    sp2.Value = er.Description;
                    lsp.Add(sp2);
                    SqlParameter sp3 = new SqlParameter();
                    sp3.ParameterName = "@Type";
                    sp3.Value = er.Type;
                    lsp.Add(sp3);
                    SqlParameter sp4 = new SqlParameter();
                    sp4.ParameterName = "@Category";
                    sp4.Value = er.Category;
                    lsp.Add(sp4);
                    SqlParameter sp5 = new SqlParameter();
                    sp5.ParameterName = "@Status";
                    sp5.Value = er.Status;
                    lsp.Add(sp5);
                    SqlParameter sp6 = new SqlParameter();
                    sp6.ParameterName = "@MakeOrPurchase";
                    sp6.Value = er.MakeOrPurchase;
                    lsp.Add(sp6);
                    SqlParameter sp7 = new SqlParameter();
                    sp7.ParameterName = "@BOMUOM";
                    sp7.Value = er.BOMUOM;
                    lsp.Add(sp7);

                    SqlParameter sp8 = new SqlParameter();
                    sp8.ParameterName = "@SalesUOM";
                    sp8.Value = er.SalesUOM;
                    lsp.Add(sp8);
                    SqlParameter sp9 = new SqlParameter();
                    sp9.ParameterName = "@PurchaseUOM";
                    sp9.Value = er.PurchaseUOM;
                    lsp.Add(sp9);

                    SqlParameter sp10 = new SqlParameter();
                    sp10.ParameterName = "@TotalAvgCost";
                    sp10.Value = er.TotalAvgCost;
                    lsp.Add(sp10);
                    SqlParameter sp11 = new SqlParameter();
                    sp11.ParameterName = "@ClassCode";
                    sp11.Value = er.ClassCode;
                    lsp.Add(sp11);
                    SqlParameter sp12 = new SqlParameter();
                    sp12.ParameterName = "@GroupCode";
                    sp12.Value = er.GroupCode;
                    lsp.Add(sp12);

                    SqlParameter sp13 = new SqlParameter();
                    sp13.ParameterName = "@RoHS";
                    sp13.Value = er.RoHS;
                    lsp.Add(sp13);
                    SqlParameter sp14 = new SqlParameter();
                    sp14.ParameterName = "@OperationLocation";
                    sp14.Value = er.OperationLocation;
                    lsp.Add(sp14);

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

        public bool CheckRecordExist(string part)
        {
            bool ret = false;

            string sqlCmd = string.Format("select [Part Number] from ItemMaster where [Part Number] = '{0}'", part);
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
