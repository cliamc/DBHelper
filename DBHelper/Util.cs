using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    public static class Util
    {
        public static bool ValidateSerialNum(string serialNumber)
        {
            if (serialNumber.Length != 10)
                return false;

            try
            {
                bool fail = false;

                string first = serialNumber.Substring(0, 5);
                string second = serialNumber.Substring(6, 4);
                string dash = serialNumber.Substring(5, 1);

                fail = dash != "-" || !first.All(char.IsNumber) || !second.All(char.IsNumber) || first.Length != 5 || second.Length != 4;

                if (fail)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public static void ExtractPartInfo(string fullPartNum, ref string partNumber, ref string version)
        {
            if (fullPartNum.Contains("="))
            {
                int nPos = fullPartNum.IndexOf("=");
                partNumber = fullPartNum.Substring(0, nPos);
                version = fullPartNum.Substring(nPos + 1, 2);

                decimal temp = Convert.ToDecimal(version);
                temp = (temp * .01m);

                version = temp.ToString();
            }
            else
            {
                partNumber = fullPartNum;
            }
        }

        public static bool VersionLarger(string partNumNew, string partNumOld)
        {
            float newVer = Convert.ToSingle(partNumNew);
            float oldVer = Convert.ToSingle(partNumOld);
            if (newVer > oldVer)
                return true;
            else
                return false;
        }

        public static int FloatStrToInt(string tf)
        {
            int pos = tf.IndexOf(".");
            string intPortion = tf.Substring(0, pos);
            int ret = Convert.ToInt32(intPortion);

            return ret;
        }

        public static string RemoveSingleQuote(string badSqlStr)
        {
            string[] tmpList = badSqlStr.Split('\'');
            string tmp = "";
            foreach (string aSub in tmpList)
            {
                tmp = tmp + aSub + " ";
            }
            return tmp.TrimEnd();
        }

        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

    } // Util class
}
