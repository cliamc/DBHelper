using System;
using System.Collections.Generic;
using System.IO;

namespace DBHelper.FileHelper
{
    public class FileDir
    {
        private string _pFolder;
        public List<string> dFiles = new List<string>();

        public void SetFolderPath(string folder)
        {
            _pFolder = folder;
        }

        public void FillEntries()
        {
            DirectoryInfo dir = new DirectoryInfo(_pFolder);

            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                //Console.WriteLine("{0}, {1}", file.Name, file.Length);
                string fqFilename = _pFolder + file.Name;
                dFiles.Add(fqFilename);
            }
        }

        // frLocation and toLocation need to be BOTH qualified file names
        public void MoveAFile(string frLocation, string toLocation)
        {
            if (!File.Exists(frLocation))
            {
                return;
            }
            // Ensure that the target does not exist.
            if (File.Exists(toLocation))
            {
                File.Delete(toLocation);
            }

            // Move the file.
            File.Move(frLocation, toLocation);
        }

        // frFolder and toFolder need to be BOTH qualified folder names
        public void MoveAFolder(string frFolder, string toFolder)
        {
            if (!Directory.Exists(frFolder))
            {
                return;
            }

            Directory.Move(frFolder, toFolder);
        }

        public void CopyAFile(string inFile, string outFile)
        {
            if (!File.Exists(inFile))
            {
                return;
            }
            // Ensure that the target does not exist.
            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            File.Copy(inFile, outFile);
        }

        // Move all files away from a given folder to a given destination folder
        public void MoveAllFiles(string frFolder, string toFolder)
        {
            if (!Directory.Exists(frFolder))
            {
                return;
            }
            if (!Directory.Exists(toFolder))
            {
                return;
            }

            string[] srcFiles = Directory.GetFiles(frFolder);
            foreach (var aFile in srcFiles)
            {
                File.Move(aFile, toFolder + Path.GetFileName(aFile));
            }
        }

    } // class
}
