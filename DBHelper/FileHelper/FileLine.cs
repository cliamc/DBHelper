using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper.FileHelper
{
    public class FileLine
    {
        private string _theSource;
        private string _theTarget;
        // Each real useful column value is separated with a ',' (comma)
        public List<LineColumn> lCollection = new List<LineColumn>();

        public void SetTheSource(string theSource)
        {
            _theSource = theSource;
        }

        // e.g. a line from a .csv file
        public void SetCollection()
        {
            String[] tokens = _theSource.Split(',');

            foreach (string aToken in tokens)
            {
                LineColumn escc = new LineColumn { cValue = aToken };
                lCollection.Add(escc);
            }

            string tmpStr = null;
            foreach (LineColumn ecv in lCollection)
            {
                tmpStr = string.Format("{0}{1},", tmpStr, ecv.cValue);
            }
            _theTarget = tmpStr.Substring(0, tmpStr.Length - 1);
        }

        // e.g. e:\Documents\Finished_Products\prodHW2\10A8\10a8j\16\dpranie-7.3.2.3.aff
        public void SetPathCollection()
        {
            String[] tokens = _theSource.Split('\\');

            lCollection.Clear();
            foreach (string aToken in tokens)
            {
                LineColumn escc = new LineColumn { cValue = aToken };
                lCollection.Add(escc);
            }
        }

        public string GetTheSource()
        {
            return _theSource;
        }

        public string GetTheTarget()
        {
            return _theTarget;
        }

        public string GetLastToken()
        {
            if (this.lCollection.Count >= 1)
                return this.lCollection[this.lCollection.Count - 1].cValue;
            else
                return "";
        }

        public string GetLastTokenExtension()
        {
            string ext = "";

            string token = this.lCollection[this.lCollection.Count - 1].cValue;
            String[] tokens = token.Split('.');                                     /// can handle the filename containing multiple dots. e.g. partnumber.version.ext
            if (tokens.Length > 1)
                ext = tokens[tokens.Length - 1];

            return ext;
        }

        public string GetLastTokenWithoutExtension()
        {
            string tot = "";

            string token = this.lCollection[this.lCollection.Count - 1].cValue;
            String[] tokens = token.Split('.');                                     /// can handle the filename containing multiple dots. e.g. partnumber.version.ext
            if (tokens.Length > 1)
            {
                for(int ct = 0; ct < tokens.Length - 1; ct++) {
                    tot = tot + tokens[ct] + ".";
                }
                tot = tot.Substring(0, tot.Length - 1);
            }
            else
            {
                tot = tokens[0];
            }

            return tot;
        }

        public string GetSecondLastToken()
        {
            if (this.lCollection.Count >= 2)
                return this.lCollection[this.lCollection.Count - 2].cValue;
            else
                return "";
        }

        // e.g. e:\Documents\dpranie.aff --- length 24, loc 12, 
        public string GetPath()
        {
            string ret = "";

            int loc = _theSource.LastIndexOf('\\');
            if (loc != -1)
            {
                ret = _theSource.Substring(0, loc + 1);
            }

            return ret;
        }

    } // class
}
