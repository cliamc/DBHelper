using System;
using System.Collections.Generic;
using System.IO;

namespace DBHelper.FileHelper
{
    public static class FileLog
    {
        private static string _PathNFile;

        internal static void SetPathNFile(string path)
        {
            _PathNFile = path;
        }
        internal static void LogMessage(string aMsg)
        {
            string localDateTime = Convert.ToString(DateTime.Now);
            File.AppendAllText(_PathNFile, string.Format("{0}: {1}\r\n", localDateTime, aMsg));
        }

    } // class
}
