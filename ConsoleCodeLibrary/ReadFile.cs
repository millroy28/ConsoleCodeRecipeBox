
using System;
using System.Collections.Generic;


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

        //public static List<string> GetFileContents(string fileName)
        //{
        //    List<string> rawFileContents = FileIO.GetFile(fileName);
        //    return rawFileContents;
        //}

        public static ColorProfile ReadColorProfile(string fileName)
        {   //CAUTION: A change to the order of the Color Profile class & color param file will require modifying this method to avoid out of range errors
            ColorProfile colors = new ColorProfile();
            List<string> colorProfileRawContents = FileIO.GetFile("config", fileName);
            
            string[] listText = colorProfileRawContents[0].Split(':');
            colors.ListText = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), listText[1]);
            
            string[] highlightedListText = colorProfileRawContents[1].Split(':');
            colors.ListTextHighlight = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), highlightedListText[1]);
           
            string[] highlightedListTextBg = colorProfileRawContents[2].Split(':');
            colors.ListTextHighlightBackground = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), highlightedListTextBg[1]);
            
            string[] border = colorProfileRawContents[3].Split(':');
            colors.Border = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), border[1]);
            
            string[] title = colorProfileRawContents[4].Split(':');
            colors.TitleText = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), title[1]);
            
            string[] properties = colorProfileRawContents[5].Split(':');
            colors.PropertyText = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), properties[1]);
            
            string[] content = colorProfileRawContents[6].Split(':');
            colors.ContentText = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), content[1]);

            string[] copyable = colorProfileRawContents[7].Split(':');
            colors.CopyText = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), copyable[1]);

            string[] navInfoTxt = colorProfileRawContents[8].Split(':');
            colors.NavText = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), navInfoTxt[1]);

            string[] navInfoTxtBg = colorProfileRawContents[9].Split(':');
            colors.NavTextBackground = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), navInfoTxtBg[1]);

            string[] menuText = colorProfileRawContents[10].Split(':');
            colors.MenuText = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), menuText[1]);

            string[] menuTextBg = colorProfileRawContents[11].Split(':');
            colors.MenuTextBackground = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), menuTextBg[1]);

            return colors;
        }

        public static string[] ReadCategories()
        {
            List<string> rawCategories = FileIO.GetFile("config", "CATEGORIES");
            string[] categories = rawCategories.ToArray();
            foreach (string c in categories)
            {
                FileIO.CategoryDirectoryCheck(c);
            }
            return categories;            
        }

        public static string[] ReadFileTitles(string category)
        {
            //DrawScreen tempDraw = new DrawScreen();
            int maxTitleLength = 30;  // DrawScreen.MainVerticalBorderLocation - 1;
            string[] fileList = FileIO.GetFileList(category);
            string[] titles = new string[fileList.Length];
            for (int i = 0; i < fileList.Length; i++)
            {
                string title = FileIO.GetFirstLine(fileList[i]);
                if(title.StartsWith(TitleBeginAndEnd) && title.EndsWith(TitleBeginAndEnd))
                {
                    char[] titleChars = title.ToCharArray();
                    string cleanTitle = "";
                    for (int j = 0; j < titleChars.Length; j++)
                    {
                        if(titleChars[j] != TitleBeginAndEnd && j < maxTitleLength)
                        {
                            cleanTitle += titleChars[j].ToString();
                        }
                    }
                    titles[i] = cleanTitle;
                }
            }
            return titles;
        }


    }
}
