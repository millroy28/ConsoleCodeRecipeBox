using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCodeLibrary
{
    class ParseCodeSnippet
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

        public static string PullLine(char beginMarker, char endMarker, List<string> rawText)
        {
            foreach(string line in rawText)
            {
                if(line.StartsWith(beginMarker) && line.EndsWith(endMarker))
                {
                    return line;
                }
            }
            return "";
        }

        public static CodeSnippet GetCodeSnippet (string fileName)
        {
            List<string> rawFileContents = GetFileContents(fileName);
            CodeSnippet thisSnippet = new CodeSnippet();

            thisSnippet.Title = PullLine(TitleBeginAndEnd, TitleBeginAndEnd, rawFileContents);
            string keywords = PullLine(KeywordsBegin, KeywordsEnd, rawFileContents);
            thisSnippet.Keywords = keywords.Replace(" ","").Split(',');
            thisSnippet.Language = PullLine(LanguageBegin, LanguageEnd, rawFileContents);

            List<string> codeBlock = new List<string>();
            bool code = false;
            foreach(string line in rawFileContents)
            {
                if (code)
                {
                    codeBlock.Add(line);
                }

                if(line == BeginCodeSection)
                {
                    code = true;
                }

            }
            thisSnippet.CodeBlock = codeBlock;

            return thisSnippet;
        }

    }
}