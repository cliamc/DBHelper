using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace DBHelper.EmpowerHelp
{
    public class BOMdata
    {
        private string wholeData = "";
        JObject jo = new JObject();

        public List<string> bomParts = new List<string>();
        public string pcbChildPN = "";
        public string pcbChildRev = "";

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

    } // class
}
