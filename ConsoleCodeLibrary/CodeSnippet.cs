using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCodeLibrary
{
    class CodeSnippet
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string[] Keywords { get; set; }
        public  List<string> CodeBlock { get; set; }

        public CodeSnippet()
        {
        }

        public CodeSnippet (string _title, string _language, string[] _keywords, List<string> _codeBlock)
        {
            Title = _title;
            Language = _language;
            Keywords = _keywords;
            CodeBlock = _codeBlock;
        }


    }
}
