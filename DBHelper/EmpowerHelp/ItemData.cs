using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DBHelper.EmpowerHelp
{
    public struct LabelField
    {
        public string PartNumber;
        public string Version;

        public string RevLetter;        // "BASE REVISION"
        public string EMC;              // REG_EMC COMPLIANT; bool
        public string EtherCat;         // REG_ETHERCAT COMPLIANT; bool
        public string InputSpecs;       // REG_INPUT SPEC
        public string OutputSpecs;      // REG_OUTPUT SPEC
        public string UL;               // REG_UL COMPLIANT; bool
        public string ROHS;             // REG_ROHS; bool
        public string TUV;              // REG_FUNCTIONAL_SAFETY; bool
        public string ProtoType;        // Status; bool
    }

    public struct PLDdata
    {
        public string Program;
        public string Filename1;
        public string Refdes1;
        public string Filename2;
        public string Refdes2;
    }

    public struct ItemDoc
    {
        public string dtype;
        public string dfile;
    }

    public class ItemData
    {
        // For Print Label application
        const string RevLetter = "BASE REVISION";
        const string EMC = "REG_EMC COMPLIANT";
        const string Ethercat = "REG_ETHERCAT COMPLIANT";
        const string InputSpecs = "REG_INPUT SPEC";
        const string OutputSpecs = "REG_OUTPUT SPEC";
        const string UL = "REG_UL COMPLIANT";
        const string ROHS = "REG_ROHS";
        const string TUV = "REG_FUNCTIONAL_SAFETY";
        const string ProtoType = "Status";
        const string trueVal = "TRUE";
        const string falseVal = "FALSE";
        const string trueValYes = "Yes";
        const string trueValOne = "1";
        const string falseValNo = "No";

        public LabelField lf = new LabelField();

        // For DocViewer application
        public List<ItemDoc> partDocs = new List<ItemDoc>();

        // For PLDDevicePI application
        const string PLD_Program = "PLD PROGRAM";
        const string PLD_Filename1 = "PLD_FILENAME_1";
        const string PLD_Refdes1 = "PLD_REFDES_1";
        const string PLD_Filename2 = "PLD_FILENAME_2";
        const string PLD_Refdes2 = "PLD_REFDES_2";

        public PLDdata pd = new PLDdata();

        // Class properties for all
        private string wholeData = "";
        JObject jo = new JObject();

        public ItemData(string wd)
        {
            this.wholeData = wd;
            jo = JObject.Parse(wd);
        }

        public bool CheckSuccess()
        {
            bool ret = false;
            string tmp = jo["Success"].ToString();
            if (string.Compare(tmp, "true", true) == 0)
            {
                ret = true;
            }

            return ret;
        }

        public bool SetupField()                            // Used in PrintLabel application. cli, 5/21/2020
        {
            bool ret = false;

            lf.PartNumber = jo["PartNumber"].ToString();         // from EmpowerItem property
            lf.Version = jo["Revision"].ToString();
            lf.ProtoType = jo["Status"].ToString();

            if (jo["Attributes"].HasValues)
            {
                ret = true;

                //string t0 = jo["Attributes"].Children().ToString();               // Newtonsoft.Json.Linq.JEnumerable`1[Newtonsoft.Json.Linq.JToken]
                //string t2 = jo["Attributes"].Values().ToString();                   // Newtonsoft.Json.Linq.JEnumerable`1[Newtonsoft.Json.Linq.JToken]

                int ct = 0;
                while (jo["Attributes"][ct].Next.HasValues)
                {
                    string name = jo["Attributes"][ct].Next["Name"].ToString();
                    string value = jo["Attributes"][ct].Next["Value"].ToString().Trim();

                    if (name.Equals(RevLetter))
                    {
                        lf.RevLetter = value;
                    }
                    else if (name.Equals(EMC))
                    {
                        if (CheckBoolValue(value))
                            lf.EMC = "True";
                        else
                            lf.EMC = "False";
                    }
                    else if (name.Equals(Ethercat))
                    {
                        if (CheckBoolValue(value))
                            lf.EtherCat = "True";
                        else
                            lf.EtherCat = "False";
                    }
                    else if (name.Equals(InputSpecs))
                    {
                        lf.InputSpecs = value;
                    }
                    else if (name.Equals(OutputSpecs))
                    {
                        lf.OutputSpecs = value;
                    }
                    else if (name.Equals(UL))
                    {
                        if (CheckBoolValue(value))
                            lf.UL = "True";
                        else
                            lf.UL = "False";
                    }
                    else if (name.Equals(ROHS))
                    {
                        if (CheckBoolValue(value))
                            lf.ROHS = "True";
                        else
                            lf.ROHS = "False";
                    }
                    else if (name.Equals(TUV))
                    {
                        if (CheckBoolValue(value))
                            lf.TUV = "True";
                        else
                            lf.TUV = "False";
                    }

                    // Check the last attribute returned from Empower, then exit
                    string lastName = jo["Attributes"].Last["Name"].ToString();
                    if (lastName.Equals(name))
                    {
                        break;
                    }

                    ct++;
                }

                // Try out to see how to get values from Empower Web Interface JSON results
                //string t41 = jo["Attributes"].First["Name"].ToString();
                //string t42 = jo["Attributes"].First["Value"].ToString();

                //string tmp2 = jo["Attributes"][31]["Name"].ToString();          // "EPICOR GROUP"

                //string t31 = (string)jo.SelectToken("Attributes[31].Name");         // EPICOR GROUP
                //string t3 = (string)jo.SelectToken("Attributes[31].Value");         // AA1
            }

            return ret;
        } // SetupField method

        public static bool CheckBoolValue(string token)
        {
            bool ret = false;
            // TRUE or Yes
            if (token.Equals(trueVal, StringComparison.OrdinalIgnoreCase)
             || token.Equals(trueValYes, StringComparison.OrdinalIgnoreCase) || token.Equals(trueValOne, StringComparison.OrdinalIgnoreCase))
                ret = true;

            return ret;
        }

        public bool SetupViewDoc()                                  // Used for eDrawings, PCB, & Schematic buttons in DocViewer application. cli, 5/21/2020
        {
            bool ret = false;
            string tmp = jo["Success"].ToString();
            if (string.Compare(tmp, "false", true) != 0)
            {
                lf.PartNumber = jo["PartNumber"].ToString();         // from EmpowerItem property
                lf.Version = jo["Revision"].ToString();
                lf.ProtoType = jo["Status"].ToString();

                if (jo["Documents"].HasValues)
                {
                    ret = true;

                    int ct = 0;
                    ItemDoc idoc = new ItemDoc();
                    while (jo["Documents"][ct].HasValues)
                    {
                        string fpath = jo["Documents"][ct]["FilePath"].ToString();
                        string ftype = jo["Documents"][ct]["Type"].ToString();
                        string err = jo["Documents"][ct]["Error"].ToString();

                        if (String.IsNullOrEmpty(fpath) && !String.IsNullOrEmpty(err))
                        {
                            // Get the file name & path from the error message text
                            int ind = err.IndexOf('\'');
                            string t1 = err.Substring(ind+1);
                            ind = t1.IndexOf('\'');
                            string t2 = t1.Substring(0, ind);

                            idoc.dtype = ftype;
                            idoc.dfile = t2;
                            partDocs.Add(idoc);
                        }
                        else
                        {
                            idoc.dtype = ftype;
                            idoc.dfile = fpath;
                            partDocs.Add(idoc);
                        }

                        // Check the last document pathAndFilename returned from Empower, then exit
                        string lastOne = jo["Documents"].Last["FilePath"].ToString();
                        if (String.IsNullOrEmpty(lastOne))
                        {
                            string lastErr = jo["Documents"].Last["Error"].ToString();
                            if (lastErr.Equals(err))
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (lastOne.Equals(fpath))
                            {
                                break;
                            }
                        }

                        ct++;
                    } // while loop
                } // if
            }

            return ret;
        } // method

        /* For PLDDevicePI application */
        public bool SetupPLD()
        {
            bool ret = false;

            if (jo["Attributes"].HasValues)
            {
                ret = true;

                int ct = 0;
                while (jo["Attributes"][ct].Next.HasValues)
                {
                    string name = jo["Attributes"][ct].Next["Name"].ToString();
                    string value = jo["Attributes"][ct].Next["Value"].ToString().Trim();

                    if (name.Equals(PLD_Program))
                    {
                        pd.Program = value;
                    }
                    else if (name.Equals(PLD_Filename1))
                    {
                        pd.Filename1 = value;
                    }
                    else if (name.Equals(PLD_Refdes1))
                    {
                        pd.Refdes1 = value;
                    }
                    else if (name.Equals(PLD_Filename2))
                    {
                        pd.Filename2 = value;
                    }
                    else if (name.Equals(PLD_Refdes2))
                    {
                        pd.Refdes2 = value;
                    }

                    // Check the last attribute returned from Empower, then exit
                    string lastName = jo["Attributes"].Last["Name"].ToString();
                    if (lastName.Equals(name))
                    {
                        break;
                    }

                    ct++;
                }
            }

            return ret;
        }

        public Tuple<string, string> GetNoteFileNames()
        {
            if (CheckSuccess())
            {
                string manuNote = "";
                string testNote = "";

                if (jo["Documents"].HasValues)
                {
                    int ct = 0;
                    while (jo["Documents"][ct].HasValues)
                    {
                        string fpath = jo["Documents"][ct]["FilePath"].ToString();
                        string goodName = "";
                        string ftype = jo["Documents"][ct]["Type"].ToString();
                        string err = jo["Documents"][ct]["Error"].ToString();

                        if (String.IsNullOrEmpty(fpath) && !String.IsNullOrEmpty(err))
                        {
                            // Get the file name & path from the error message text
                            int ind = err.IndexOf('\'');
                            string t1 = err.Substring(ind + 1);
                            ind = t1.IndexOf('\'');
                            goodName = t1.Substring(0, ind);
                        }
                        else
                        {
                            goodName = fpath;
                        }

                        if (ftype.Equals("Manufacturing Notes"))
                        {
                            manuNote = goodName;
                        }
                        else if (ftype.Equals("Test Notes"))
                        {
                            testNote = goodName;
                        }

                        // Check the last document pathAndFilename returned from Empower, then exit
                        string lastOne = jo["Documents"].Last["FilePath"].ToString();
                        if (String.IsNullOrEmpty(lastOne))
                        {
                            string lastErr = jo["Documents"].Last["Error"].ToString();
                            if (lastErr.Equals(err))
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (lastOne.Equals(fpath))
                            {
                                break;
                            }
                        }

                        ct++;
                    } // while loop
                } // if

                return new Tuple<string, string>(manuNote, testNote);
            }
            else
            {
                return new Tuple<string, string>("", "");
            }
        }

    } // class
}
