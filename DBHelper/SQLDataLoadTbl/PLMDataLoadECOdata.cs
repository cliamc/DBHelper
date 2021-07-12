using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBHelper.SQLTable
{
    public struct ECORecord
    {
        public string ECONumber;
        public string DescriptionOfChanges;
        public string Reason;
        public string DesignNotes;
        public string ECOPotentialInternalImpact;
        public string FinalMaterialDispositionNotes;
        public string SpecialInstructions;
    }

    public class PLMDataLoadECOdata
    {
        private AccessSQL dbAccess = new AccessSQL();

        public PLMDataLoadECOdata()
        {
            dbAccess.SetConnStr(DBConnectionStr.ConnectionStrPLMDataLoad);
        }

        public bool UpdateTbl(ECORecord er)
        {
            bool ret = false;

            try
            {
                if (CheckRecordExist(er.ECONumber))
                {
                    //string sqlCmd = string.Format("Update ECO_AllCompleted set Description = @DOC, Reason = @Reason, [Design Notes] = @DN," +
                    //                              " [Potential Internal Impact] = @EPII, [Final Material Disposition Notes] = @FMDN,  [Special Instructions] = @SI" +
                    //                              " where [ECO Number] = @ECONumber");
                    string sqlCmd = string.Format("Update ECO_AllCompleted set Description = @DOC, Reason = @Reason, [Design Notes] = @DN," +
                              " [Potential Internal Impact] = @EPII, [Final Material Disposition Notes] = @FMDN,  [Special Instructions] = @SI" +
                              " where [ECO Number] = @ECONumber");

                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@ECONumber";
                    sp0.Value = er.ECONumber;
                    lsp.Add(sp0);
                    SqlParameter sp1 = new SqlParameter();
                    sp1.ParameterName = "@DOC";
                    sp1.Value = er.DescriptionOfChanges;
                    lsp.Add(sp1);
                    SqlParameter sp2 = new SqlParameter();
                    sp2.ParameterName = "@Reason";
                    sp2.Value = er.Reason;
                    lsp.Add(sp2);
                    SqlParameter sp3 = new SqlParameter();
                    sp3.ParameterName = "@DN";
                    sp3.Value = er.DesignNotes;
                    lsp.Add(sp3);
                    SqlParameter sp4 = new SqlParameter();
                    sp4.ParameterName = "@EPII";
                    sp4.Value = er.ECOPotentialInternalImpact;
                    lsp.Add(sp4);
                    SqlParameter sp5 = new SqlParameter();
                    sp5.ParameterName = "@FMDN";
                    sp5.Value = er.FinalMaterialDispositionNotes;
                    lsp.Add(sp5);
                    SqlParameter sp6 = new SqlParameter();
                    sp6.ParameterName = "@SI";
                    sp6.Value = er.SpecialInstructions;
                    lsp.Add(sp6);

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
        } // update

        public bool InsertTbl(ECORecord er)
        {
            bool ret = false;

            try
            {
                if (!CheckRecordExist(er.ECONumber))
                {
                    string sqlCmd = string.Format("insert into ECOdata (ECONumber, CompletePN, BaseModelPN, CustomerPNExt, ChangeDescription, Status, AssignedTo, Priority, DesignType)"
                                                + " values (@ECO, @CompletePN, @BaseModelPN, @CustomerPNExt, @ChangeDescription, @Status, @AssignedTo, @Priority, @DesignTyope)");
                    dbAccess.SetQueryCmd(sqlCmd);

                    List<SqlParameter> lsp = new List<SqlParameter>();
                    SqlParameter sp0 = new SqlParameter();
                    sp0.ParameterName = "@ECO";
                    sp0.Value = er.ECONumber;
                    lsp.Add(sp0);
                    //SqlParameter sp1 = new SqlParameter();
                    //sp1.ParameterName = "@CompletePN";
                    //sp1.Value = er.CompletePN;
                    //lsp.Add(sp1);
                    //SqlParameter sp2 = new SqlParameter();
                    //sp2.ParameterName = "@BaseModelPN";
                    //sp2.Value = er.BaseModelPN;
                    //lsp.Add(sp2);
                    //SqlParameter sp3 = new SqlParameter();
                    //sp3.ParameterName = "@CustomerPNExt";
                    //sp3.Value = er.CustomerPNExt;
                    //lsp.Add(sp3);
                    //SqlParameter sp4 = new SqlParameter();
                    //sp4.ParameterName = "@ChangeDescription";
                    //sp4.Value = er.ChangeDescription;
                    //lsp.Add(sp4);
                    //SqlParameter sp5 = new SqlParameter();
                    //sp5.ParameterName = "@Status";
                    //sp5.Value = er.Status;
                    //lsp.Add(sp5);
                    //SqlParameter sp6 = new SqlParameter();
                    //sp6.ParameterName = "@AssignedTo";
                    //sp6.Value = er.AssignedTo;
                    //lsp.Add(sp6);
                    //SqlParameter sp7 = new SqlParameter();
                    //sp7.ParameterName = "@Priority";
                    //sp7.Value = er.DesignTyope;
                    //lsp.Add(sp7);
                    //SqlParameter sp8 = new SqlParameter();
                    //sp8.ParameterName = "@DesignTyope";
                    //sp8.Value = er.DesignTyope;
                    //lsp.Add(sp8);

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
        } // insert

        private bool CheckRecordExist(string eco)
        {
            bool ret = false;

            string sqlCmd = string.Format("select [ECO Number] from ECO_AllCompleted where [ECO Number] = '{0}'", eco);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                ret = true;
            }

            return ret;
        }

        public string GetChangeDescription(string eco)
        {
            string sqlCmd = string.Format("select Description from ECO_AllCompleted where [ECO Number] = '{0}'", eco);
            dbAccess.SetQueryCmd(sqlCmd);
            object retVal = dbAccess.GetASingleValue();
            if (retVal != null)
            {
                return retVal.ToString();
            }

            return "";
        }

    } // class
}
