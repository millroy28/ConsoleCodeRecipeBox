
using System;


namespace ConsoleCodeLibrary
{
    class Program
    {
        //establish tags for file read control
        
        static void Main(string[] args)
        {
            //Flow:
            //Main -> InitializeConsoleDisplay - sets the console display and font size to avoid display errors and universalize experience
            //Main -> Controller - Starts flow of program
            //Controller -> ParseCodeSnippet - Grab Welcome.txt
            //Controller -> FileIO.GetFileList - Grabs list of files from which to browse
            //Controller -> DrawScreen - DWISOTB
            //Controller -> Controller.AwaitKeyPress

            
            int[] consoleSize = InitializeConsoleDisplay.SetDisplayAndFontSize();
            Controller.Run(consoleSize);
            
            
            /*
            Console.WriteLine("Hello World!");
            string message = "Check your clipboard for this text!";
            Console.WriteLine(message);

            var clipboardService = new WindowsClipboardService(timeout:TimeSpan.FromMilliseconds(200));
            clipboardService.SetTextAsync(message);
            */
            
        }
    }
}
