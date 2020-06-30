using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace ConsoleCodeLibrary
{
    class FileIO
    {
        public static List<string> GetFile(string directory, string fileName)
        {
            StreamReader reader = new StreamReader($"../../../{directory}/{fileName}.txt");
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

        public static string[] GetFileList(string directory)
        {
            string[] fileList = Directory.GetFiles($"../../../{directory}/");
            return fileList;
        }
    }
}
