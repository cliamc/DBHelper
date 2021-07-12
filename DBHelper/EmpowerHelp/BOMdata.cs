using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace DBHelper.EmpowerHelp
{
    public struct PLDparams
    {
        public string ChildPN1;
        public string ChildPN2;
    }

    public class BOMdata
    {
        private string wholeData = "";
        JObject jo = new JObject();

        // For DocViewer application
        public List<string> bomParts = new List<string>();
        public string pcbChildPN = "";
        public string pcbChildRev = "";

        // For PLDDevicePI application; cli, 2/11/2021
        public PLDparams pp = new PLDparams();

        public BOMdata(string wd)
        {
            this.wholeData = wd;
            jo = JObject.Parse(wd);
        }

        public bool SetupBOMChildPN()                                  // Used in DocViewer application. cli, 5/21/2020
        {
            bool ret = false;
            string tmp = jo["Success"].ToString();
            if (string.Compare(tmp, "false", true) != 0)
            {
                string ParentPN = jo["ParentPN"].ToString();            // from EmpowerItem result header
                string ParentRev = jo["ParentRev"].ToString();

                if (jo["BOMs"].HasValues)
                {
                    int ct = 0;

                    while (jo["BOMs"][ct].HasValues)
                    {
                        string cpn = jo["BOMs"][ct]["ChildPN"].ToString();
                        string crev = jo["BOMs"][ct]["ChildRev"].ToString();
                        string Refdes = jo["BOMs"][ct]["Refdes"].ToString();

                        if (string.Equals(Refdes, "PCB", StringComparison.OrdinalIgnoreCase))
                        {
                            pcbChildPN = cpn;
                            pcbChildRev = crev;
                            ret = true;

                            break;
                        }

                        bomParts.Add(cpn);

                        // Check the last document pathAndFilename returned from Empower, then exit
                        string lastOne = jo["BOMs"].Last["ChildPN"].ToString();
                        if (lastOne.Equals(cpn))
                        {
                            break;
                        }

                        ct++;
                    } // while loop
                } // if
            }

            return ret;
        } // method

        public bool GetPLDamcPN(string rd1, string rd2)
        {
            bool ret = false;

            this.pp.ChildPN1 = "";
            this.pp.ChildPN2 = "";

            string tmp = jo["Success"].ToString();
            if (string.Compare(tmp, "false", true) != 0)
            {
                if (jo["BOMs"].HasValues)
                {
                    int ct = 0;
                    while (jo["BOMs"][ct].HasValues)
                    {
                        string cpn = jo["BOMs"][ct]["ChildPN"].ToString();
                        string Refdes = jo["BOMs"][ct]["Refdes"].ToString();

                        if (!String.IsNullOrEmpty(rd1) && Refdes.Contains(rd1))
                        //if (string.Equals(Refdes, rd1, StringComparison.OrdinalIgnoreCase))
                        {
                            this.pp.ChildPN1 = cpn;
                            ret = true;

                            //break;
                        }
                        //else
                        //if (string.Equals(Refdes, rd2, StringComparison.OrdinalIgnoreCase))
                        if (!String.IsNullOrEmpty(rd2) && Refdes.Contains(rd2))
                        {
                            this.pp.ChildPN2 = cpn;
                            ret = true;

                            //break;
                        }

                        // Check the last BOMs element returned from Empower, then exit
                        string lastOne = jo["BOMs"].Last["ChildPN"].ToString();
                        if (lastOne.Equals(cpn))
                        {
                            break;
                        }

                        //if ( (this.pp.ChildPN1 != "") && (this.pp.ChildPN2 != "") )
                        //{
                        //    break;
                        //}

                        ct++;
                    } // while loop
                } // if
            }

            return ret;
        } // GetPLDamcPN

    } // class
}
