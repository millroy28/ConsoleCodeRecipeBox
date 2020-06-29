using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCodeLibrary
{
    class ReadFile
    {
        public const char TitleBeginAndEnd = '\'';
        public const char CommentBeginAndEnd = '/';
        public const char KeywordsBegin = '<';
        public const char KeywordsEnd = '>';
        public const char LanguageBegin = '[';
        public const char LanguageEnd = ']';
        public const string BeginCodeSection = ":::Code:::";

        public static List<string> GetFileContents(string fileName)
        {
            List<string> rawFileContents = FileIO.GetFile(fileName);
            return rawFileContents;
        }
  
        



    }
}
