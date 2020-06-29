using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace ConsoleCodeLibrary
{
    class FileIO
    {
        public static List<string> GetFile(string fileName)
        {
            StreamReader reader = new StreamReader($"../../../stash/{fileName}.txt");
            string line = reader.ReadLine();
            List<string> fileContents = new List<string>();
            do
            {
                fileContents.Add(line);
                line = reader.ReadLine();
            } while (line != null);
            reader.Close();
            return fileContents;
        }

        public static string[] GetFileList()
        {
            string[] fileList = Directory.GetFiles("../../../stash/");
            return fileList;
        }
    }
}
