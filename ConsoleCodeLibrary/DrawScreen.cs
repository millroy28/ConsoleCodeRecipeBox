using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleCodeLibrary
{
    class DrawScreen
    {
        public static int XMax { get; set; }
        public static int YMax { get; set; }
        public static int XListStart { get; set; }
        public static int YListStart { get; set; }
        public static int MainHorizontalBorderLocation { get; set; }
        public static int MainVerticalBorderLocation { get; set; }
        public static char MainHorizontalBorderCharacter { get; set; }
        public static char MainVerticalBorderCharacter { get; set; }
        public static int MaxListLength { get; set; }
        public ColorProfile Colors { get; set; }
        public string CategoryName { get; set; }
        public int ListPage { get; set; }
        public int Selection { get; set; }
        public int ContentPage { get; set; }
        public int Focus { get; set; }
        public List<KeyValuePair<string, string>> FilesAndTitles { get; set; }
        public int ListStatus { get; set; }
        public List<NoteObject> Snippets { get; set; }

        public string ContentForClipboard { get; set; }


        //Default Constructor
        public DrawScreen()
        {
        }
        //Constructor
        public DrawScreen(int[] _screenParams, ColorProfile _colors, string _categoryName, List<KeyValuePair<string, string>> _filesAndTitles, List<NoteObject> _snippets)
        {
            XMax = _screenParams[0];
            YMax = _screenParams[1];
            XListStart = 1;
            YListStart = 2;
            MainHorizontalBorderLocation = 1;
            MainVerticalBorderLocation = 31;
            MainHorizontalBorderCharacter = '=';
            MainVerticalBorderCharacter = '|';
            MaxListLength = 0;
            Colors = _colors;
            CategoryName = _categoryName;
            ListPage = 0;
            Selection = 0;
            ContentPage = 0;
            Focus = 0;
            FilesAndTitles = _filesAndTitles;
            ListStatus = 0;
            Snippets = _snippets;
            ContentForClipboard = "";
        }

        public void MoveListSelection(bool up)
        {

            Console.SetCursorPosition(XListStart, YListStart + Selection);
            Console.Write("                              ");
            Console.SetCursorPosition(XListStart, YListStart + Selection);
            Console.ForegroundColor = Colors.ListTextHighlight;
            Console.BackgroundColor = Colors.ListTextHighlightBackground;
            Console.Write(FilesAndTitles[Selection + (MaxListLength * ListPage)].Value);
            Console.ResetColor();

            Console.ForegroundColor = Colors.ListText;
            if (up)
            {
                Console.SetCursorPosition(XListStart, YListStart + Selection + 1);
                Console.Write("                              ");
                Console.SetCursorPosition(XListStart, YListStart + Selection + 1);
                Console.Write(FilesAndTitles[Selection + (MaxListLength * ListPage) + 1].Value);
            }
            else
            {
                Console.SetCursorPosition(XListStart, YListStart + Selection - 1);
                Console.Write("                              ");
                Console.SetCursorPosition(XListStart, YListStart + Selection - 1);
                Console.Write(FilesAndTitles[Selection + (MaxListLength * ListPage) - 1].Value);
            }
            Console.ResetColor();
        }

        public void PrintList () //+++++++++++++++++Move the menu printing into its own method below and make it print either based on 
        {
            //ListStatus = -1 when on the last page of the list. Controller will break on a -1 when PgDn is pressed here disallowing further scrolling.
            //ListStatus = 1 when on the first page of the list. Controller will break on a 1 when PgUp is pressed here disallowing further scrolling.
            //ListStatus = 0 otherwise, allowing scrolling either way.
            //ListStatus = 2 if the list is too small for scrolling

            int listStartIndex = MaxListLength * ListPage;

            //clear list on display
            for (int i = YListStart; i < YMax; i++)
            {
                Console.SetCursorPosition(XListStart, i);
                Console.Write("                              ");
            }
            
            for (int i = YListStart, j = listStartIndex; i < YMax; i++, j++)
            {
                Console.SetCursorPosition(XListStart, i);
                if (j == FilesAndTitles.Count)  //If end of index is reached
                {
                    Console.SetCursorPosition(XListStart, YMax - 1);
                    if(ListPage == 0) 
                    {
                        Console.BackgroundColor = Colors.MenuTextBackground;
                        Console.ForegroundColor = Colors.MenuText;
                        Console.Write($"PAGE {ListPage + 1}                        ");
                        Console.ResetColor();
                        ListStatus = 2;
                        return;
                    }
                    else
                    {
                        Console.BackgroundColor = Colors.MenuTextBackground;
                        Console.ForegroundColor = Colors.MenuText;
                        Console.Write($"PAGE {ListPage + 1} PgUp: Prev             ");
                        Console.ResetColor();
                        ListStatus = -1;
                        return;
                    }

                }
                
                if(j == listStartIndex + MaxListLength) //If end of display length is reached
                {
                    if (ListPage == 0)
                    {
                        Console.BackgroundColor = Colors.MenuTextBackground;
                        Console.ForegroundColor = Colors.MenuText;
                        Console.Write($"PAGE {ListPage + 1}              PgDn: Next");
                        Console.ResetColor();
                        ListStatus = 1;
                        return;
                    }
                    else
                    {
                        Console.BackgroundColor = Colors.MenuTextBackground;
                        Console.ForegroundColor = Colors.MenuText;
                        Console.Write($"PAGE {ListPage + 1} PgUp: Prev / PgDn: Next");
                        Console.ResetColor();
                        ListStatus = 0;
                        return;
                    }
                }
                Console.ForegroundColor = Colors.ListText;
                Console.Write(FilesAndTitles[j].Value);
            }
            ListStatus = 0;
            return;
        }
        public void DrawNavBar()
        {

        }
        public void DrawBorders ()
        {
            ClearContentWindow();
            Console.ForegroundColor = Colors.Border;
            VerticleBorder(YMax, MainVerticalBorderLocation, MainHorizontalBorderLocation, MainVerticalBorderCharacter);
            HorizontalBorder(XMax, MainHorizontalBorderLocation, MainHorizontalBorderCharacter);
            PrintCategoryTitle(Colors.TitleText, CategoryName);
            Console.ResetColor();

            MaxListLength = YMax - 1 - 1 - MainHorizontalBorderLocation;
        }
        public static void VerticleBorder(int height, int vertLocation, int horizLocation, char character)
        {
            for (int i = horizLocation; i < height; i++)
            {
                Console.SetCursorPosition(vertLocation, i);
                Console.Write(character);
            }
        }
        public static void HorizontalBorder(int width, int location, char character)
        {
            for(int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i, location);
                Console.Write(character);
            }
        }

        public static void PrintCategoryTitle(ConsoleColor color, string category)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(2, MainHorizontalBorderLocation);
            Console.Write(category.ToUpper());
            Console.ResetColor();
        }

        public void PrintContentsHeader()
        {
            ClearContentWindow();
            int index = Selection + (MaxListLength * ListPage);
            
            //Print Title
            Console.ForegroundColor = Colors.TitleText;
            string title = Snippets[index].Title;
            int titleStartX = ((XMax - MainVerticalBorderLocation-title.Length) / 2) + MainVerticalBorderLocation;
            int titleStartY = MainHorizontalBorderLocation + 1;
            Console.SetCursorPosition(titleStartX, titleStartY);
            Console.Write(title);

            //Print Languages & Keywords
            Console.ForegroundColor = Colors.PropertyText;
            string languages = "";
            for (int i = 0; i < Snippets[index].Language.Length; i++)
            {
                languages += Snippets[index].Language[i];
                if(i + 1 < Snippets[index].Language.Length)
                {
                    languages += ", ";
                }
            }
            int languageStartX = MainVerticalBorderLocation + 2;
            int languageStartY = MainHorizontalBorderLocation + 2;
            Console.SetCursorPosition(languageStartX, languageStartY);
            Console.Write(languages);

            string keywords = "";
            for (int i = 0; i < Snippets[index].Keywords.Length; i++)
            {
                keywords += Snippets[index].Keywords[i];
                if (i + 1 < Snippets[index].Keywords.Length)
                {
                    keywords += ", ";
                }
            }
            int keywordsStartX = XMax - (keywords.Length + 2);
            int keywordsStartY = MainHorizontalBorderLocation + 2;
            Console.SetCursorPosition(keywordsStartX, keywordsStartY);
            Console.Write(keywords);

            //Draw content bottom border
            Console.ForegroundColor = Colors.Border;
            int secondaryBorderStartX = MainVerticalBorderLocation + 1;
            int secondaryBorderStartY = MainHorizontalBorderLocation + 3;
            for (int i = secondaryBorderStartX; i < XMax; i++)
            {
                Console.SetCursorPosition(i, secondaryBorderStartY);
                Console.Write("-");
            }

            PrintContentsBody(index);
        }

        public void PrintContentsBody(int index)
        {
            //clears content for clipboard
            ContentForClipboard = "";
            //prints first contents page
            Console.ForegroundColor = Colors.ContentText;
            int x = MainVerticalBorderLocation + 3;
            int y = MainHorizontalBorderLocation + 5;
            int maxLineLength = XMax - x - 2;
            bool addToCopyString = false;

            foreach (string s in Snippets[index].Contents[0].ContentBlock)
            {
                if (s == ReadFile.BeginCodeSection)
                {
                    Console.ForegroundColor = Colors.CopyText;
                    addToCopyString = true;
                }
                else if (s == ReadFile.EndCodeSection)
                {
                    Console.ForegroundColor = Colors.ContentText;
                    addToCopyString = false;
                }
                else if (s.Length > maxLineLength) //if the Line will run off the screen. Not the best code - only handles one overrun
                {
                    if (addToCopyString)
                    {
                        ContentForClipboard += s + '\n';
                    }

                    string substring1 = s.Substring(0, maxLineLength);
                    string substring2 = s.Substring(maxLineLength);
                    Console.SetCursorPosition(x, y);
                    Console.Write(substring1);
                    y++;
                    Console.SetCursorPosition(x, y);
                    Console.Write(substring2);
                    y++;
                } 
                else
                {
                    if (addToCopyString)
                    {
                        ContentForClipboard += s + '\n';
                    }

                    Console.SetCursorPosition(x, y);
                    Console.Write(s);
                    y++;
                }
            }
            Console.ResetColor();
        }
        public void ClearContentWindow()
        {
            for(int y = MainHorizontalBorderLocation + 1; y < YMax; y++)
            {
                for(int x = MainVerticalBorderLocation + 1; x < XMax; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

    }
}
