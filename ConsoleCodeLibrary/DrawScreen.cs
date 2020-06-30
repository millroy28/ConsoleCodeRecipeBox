using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
        //Default Constructor
        public DrawScreen()
        {

        }
        //Constructor
        public DrawScreen(int[] screenParams)
        {
            XMax = screenParams[0];
            YMax = screenParams[1];
            XListStart = 1;
            YListStart = 2;
            MainHorizontalBorderLocation = 1;
            MainVerticalBorderLocation = 31;
            MainHorizontalBorderCharacter = '=';
            MainVerticalBorderCharacter = '|';
        }

        public void MoveListSelection (int selection, bool up, string previousListItem, string selectedListItem)
        {

            Console.SetCursorPosition(XListStart, YListStart + selection);
            Console.Write("                              ");
            Console.SetCursorPosition(XListStart, YListStart + selection);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(selectedListItem);
            Console.ResetColor();


            Console.ForegroundColor = ConsoleColor.Yellow;
            if (up)
            {
                Console.SetCursorPosition(XListStart, YListStart + selection + 1);
                Console.Write("                              ");
                Console.SetCursorPosition(XListStart, YListStart + selection + 1);
                Console.Write(previousListItem);
            }
            else
            {
                Console.SetCursorPosition(XListStart, YListStart + selection - 1);
                Console.Write("                              ");
                Console.SetCursorPosition(XListStart, YListStart + selection - 1);
                Console.Write(previousListItem);
            }
            Console.ResetColor();
        }

        public int PrintList (int page, string[] names)
        {
            //Returns -1 when on the last page of the list. Controller will break on a -1 when PgDn is pressed here disallowing further scrolling.
            //Returns 1 when on the first page of the list. Controller will break on a 1 when PgUp is pressed here disallowing further scrolling.
            //Returns 0 otherwise, allowing scrolling either way.
            //Returns 2 if the list is too small for scrolling

            int listMaxLength = YMax - YListStart - 1;
            int listStartIndex = listMaxLength * page;

            //clear list on display
            for (int i = YListStart; i < YMax; i++)
            {
                Console.SetCursorPosition(XListStart, i);
                Console.Write("                              ");
            }
            
            for (int i = YListStart, j = listStartIndex; i < YMax; i++, j++)
            {
                Console.SetCursorPosition(XListStart, i);
                if (j == names.Length)  //If end of index is reached
                {
                    Console.SetCursorPosition(XListStart, YMax - 1);
                    if(page == 0) 
                    {
                        return 2;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"PAGE {page + 1} PgUp: Prev             ");
                        Console.ResetColor();
                        return -1;
                    }

                }
                
                if(j == listStartIndex + listMaxLength) //If end of display length is reached
                {
                    if (page == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"PAGE {page + 1}              PgDn: Next");
                        Console.ResetColor();
                        return 1;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"PAGE {page + 1} PgUp: Prev / PgDn: Next");
                        Console.ResetColor();
                        return 0;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(names[j]);
            }
            return 0;
        }
        public int DrawBorders ()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            VerticleBorder(YMax, MainVerticalBorderLocation, MainHorizontalBorderLocation, MainVerticalBorderCharacter);
            HorizontalBorder(XMax, MainHorizontalBorderLocation, MainHorizontalBorderCharacter);
            Console.ResetColor();

            int displayLinesAvailable = YMax - 1 - 1 - MainHorizontalBorderLocation;
            return displayLinesAvailable;
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
       
    }
}
