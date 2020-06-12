using System;
using System.Collections.Generic;
using System.IO;

namespace DBHelper.FileHelper
{
    public class FileObj
    {
        public string _fileName;
        public List<FileLine> fFile = new List<FileLine>();

        public void SetFileName(string fileName)
        {
            _fileName = fileName;
        }
        public virtual void ReadContent()
        {
            //Console.WriteLine(_fileName + " in EricssonFile.ReadContent");
            foreach (string aLine in File.ReadLines(_fileName))
            {
                FileLine esLine = new FileLine();
                esLine.SetTheSource(aLine);
                esLine.SetCollection();

                fFile.Add(esLine);
            }
        }

        public virtual void WriteContent()
        {
            //Console.WriteLine("--- in WriteContent");
            List<string> tmpObj = new List<String>();

            foreach (FileLine esLine in fFile)
            {
                string tmpStr = esLine.GetTheTarget();
                tmpObj.Add(tmpStr);
            }

            File.WriteAllLines(_fileName, tmpObj);
        }

        public string ReadFileToString()
        {
            string ret = File.ReadAllText(this._fileName);
            return ret;
        }

    } // class
}
