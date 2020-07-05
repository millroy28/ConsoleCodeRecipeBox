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
        public int Page { get; set; }
        public int Selection { get; set; }
        public List<KeyValuePair<string, string>> FilesAndTitles { get; set; }
        public int ListStatus { get; set; }


        //Default Constructor
        public DrawScreen()
        {
            //XListStart = 1;
            //YListStart = 2;
            //MainHorizontalBorderLocation = 1;
            //MainVerticalBorderLocation = 31;
            //MainHorizontalBorderCharacter = '=';
            //MainVerticalBorderCharacter = '|';
        }
        //Constructor
        public DrawScreen(int[] _screenParams, ColorProfile _colors, string _categoryName, List<KeyValuePair<string, string>> _filesAndTitles)
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
            Page = 0;
            Selection = 0;
            FilesAndTitles = _filesAndTitles;
            ListStatus = 0;
        }

        public void MoveListSelection(bool up)
        {

            Console.SetCursorPosition(XListStart, YListStart + Selection);
            Console.Write("                              ");
            Console.SetCursorPosition(XListStart, YListStart + Selection);
            Console.ForegroundColor = Colors.ListTextHighlight;
            Console.BackgroundColor = Colors.ListTextHighlightBackground;
            Console.Write(FilesAndTitles[Selection + (MaxListLength * Page)].Value);
            Console.ResetColor();

            Console.ForegroundColor = Colors.ListText;
            if (up)
            {
                Console.SetCursorPosition(XListStart, YListStart + Selection + 1);
                Console.Write("                              ");
                Console.SetCursorPosition(XListStart, YListStart + Selection + 1);
                Console.Write(FilesAndTitles[Selection + (MaxListLength * Page) + 1].Value);
            }
            else
            {
                Console.SetCursorPosition(XListStart, YListStart + Selection - 1);
                Console.Write("                              ");
                Console.SetCursorPosition(XListStart, YListStart + Selection - 1);
                Console.Write(FilesAndTitles[Selection + (MaxListLength * Page) - 1].Value);
            }
            Console.ResetColor();
        }

        public void PrintList ()//int page, string[] names)
        {
            //Returns -1 when on the last page of the list. Controller will break on a -1 when PgDn is pressed here disallowing further scrolling.
            //Returns 1 when on the first page of the list. Controller will break on a 1 when PgUp is pressed here disallowing further scrolling.
            //Returns 0 otherwise, allowing scrolling either way.
            //Returns 2 if the list is too small for scrolling

            
            int listStartIndex = MaxListLength * Page;

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
                    if(Page == 0) 
                    {
                        ListStatus = 2;
                        return;
                    }
                    else
                    {
                        Console.BackgroundColor = Colors.MenuTextBackground;
                        Console.ForegroundColor = Colors.MenuText;
                        Console.Write($"PAGE {Page + 1} PgUp: Prev             ");
                        Console.ResetColor();
                        ListStatus = -1;
                        return;
                    }

                }
                
                if(j == listStartIndex + MaxListLength) //If end of display length is reached
                {
                    if (Page == 0)
                    {
                        Console.BackgroundColor = Colors.MenuTextBackground;
                        Console.ForegroundColor = Colors.MenuText;
                        Console.Write($"PAGE {Page + 1}              PgDn: Next");
                        Console.ResetColor();
                        ListStatus = 1;
                        return;
                    }
                    else
                    {
                        Console.BackgroundColor = Colors.MenuTextBackground;
                        Console.ForegroundColor = Colors.MenuText;
                        Console.Write($"PAGE {Page + 1} PgUp: Prev / PgDn: Next");
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
        public void DrawBorders ()
        {
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

        public void PrintFile(int selection)
        {
            
        }
       
    }
}
